using System;
using System.IO;
using System.Collections.Generic;
using System.Net;
using UnityEditor;
using UnityEngine;
using VRC.SDK3.Avatars.Components;
using VRC.SDK3A.Editor;
using VRC.SDKBase.Editor.Api;
using VRC.Core;

namespace Sayabeans.UploadWithoutPreCheck.Editor
{
    internal static class UploadWithoutPreCheck
    {
        private const string menuName = "GameObject/[VRChat] Upload without pre-check";
        private const int menuPriority = 21;

        [MenuItem(menuName, true, menuPriority)]
        internal static bool ValidateUpload(MenuCommand cmd)
        {
            if (Selection.objects.Length != 1 || Selection.transforms.Length != 1)
            {
                return false;
            }

            return Selection.activeGameObject.activeInHierarchy && Selection.activeGameObject.GetComponent<VRCAvatarDescriptor>() != null;
        }

        [MenuItem(menuName, false, menuPriority)]
        internal static async void Upload()
        {
            IVRCSdkAvatarBuilderApi _builder = null;
            var targetAvatar = Selection.activeGameObject;
            var pipelineManager = targetAvatar.GetComponent<PipelineManager>();
            VRCAvatar vrcAvatar = default;
            string thumbnailPath = null;

            if (EditorApplication.isPlayingOrWillChangePlaymode)
            {
                EditorUtility.DisplayDialog("Cannot Upload", "Please exit Play Mode first.\n\n"
                + "アップロードするには、Play Modeを終了してください。", "OK");
                return;
            }

            if (VRCSdkControlPanel.window == null)
            {
                EditorUtility.DisplayDialog("Cannot Upload", "Please open VRCSDK Control Panel first.\n\n"
                + "先に、VRCSDKのコントロールパネルを開いておいてください。", "OK");
                return;
            }

            if (pipelineManager == null)
            {
                targetAvatar.AddComponent<PipelineManager>();
            }

            if (!VRCSdkControlPanel.TryGetBuilder(out _builder))
            {
                //I don't know when fail this.
                EditorUtility.DisplayDialog("Cannot Upload", "Failed to get builder. Please contact tool author.\n\n"
                + "Builderの取得に失敗しました。ツール制作者に報告してください。", "OK");
                return;
            }

            try
            {
                if (!string.IsNullOrWhiteSpace(pipelineManager.blueprintId))
                {
                    try
                    {
                        vrcAvatar = await VRCApi.GetAvatar(pipelineManager.blueprintId, true);
                    }
                    catch (ApiErrorException ex)
                    {
                        if (ex.StatusCode != HttpStatusCode.NotFound)
                        {
                            throw;
                        }
                        Debug.Log("catched");
                    }
                }

                //new Avatar
                if (string.IsNullOrEmpty(vrcAvatar.ID))
                {
                    if (AvatarInfo.GetSelectedAvatarInControlPanel() != targetAvatar)
                    {
                        EditorUtility.DisplayDialog("Upload Canceled", "You are trying to upload a new avatar.\n"
                        + "To get the new avatar's information from the VRCSDK Control Panel, "
                        + "please select the target avatar in the VRCSDK Control Panel, enter the information, and run again.\n\n"
                        + "新しいアバターをアップロードしようとしています。\nVRCSDKのコントロールパネルから新しいアバターの情報を取得するため、"
                        + "コントロールパネルでアップロード対象のアバターを選択し、情報を入力してから再度実行してください。", "OK");
                        return;
                    }

                    if (string.IsNullOrWhiteSpace(AvatarBuilderSessionState.AvatarName))
                    {
                        EditorUtility.DisplayDialog("Upload Canceled", "You are trying to upload a new avatar.\n"
                        + "To upload a new avatar, you must set the avatar name.\n"
                        + "Please set the avatar name in the VRCSDK Control Panel.\n\n"
                        + "新しいアバターをアップロードしようとしています。\n新しいアバターをアップロードするには、アバター名を入力する必要があります。\n"
                        + "VRCSDKのコントロールパネルでアバター名を入力してください。", "OK");
                        return;
                    }

                    thumbnailPath = AvatarBuilderSessionState.AvatarThumbPath;

                    if (string.IsNullOrEmpty(thumbnailPath))
                    {
                        EditorUtility.DisplayDialog("Upload Canceled", "You are trying to upload a new avatar.\n"
                        + "To upload a new avatar, you must prepare the avatar thumbnail.\n"
                        + "Please prepare the avatar thumbnail in the VRCSDK Control Panel.\n\n"
                        + "新しいアバターをアップロードしようとしています。\n新しいアバターをアップロードするには、アバターのサムネイル画像を用意する必要があります。\n"
                        + "VRCSDKのコントロールパネルでアバターのサムネイル画像を用意してください。", "OK");
                        return;
                    }

                    var tags = new List<string>(AvatarBuilderSessionState.AvatarTags.Split('|'));
                    tags.RemoveAll(x => x == "");

                    vrcAvatar = new VRCAvatar
                    {
                        Name = AvatarBuilderSessionState.AvatarName,
                        Description = AvatarBuilderSessionState.AvatarDesc,
                        Tags = tags,
                        ReleaseStatus = AvatarBuilderSessionState.AvatarReleaseStatus,
                    };
                }

                await _builder.BuildAndUpload(targetAvatar, vrcAvatar, thumbnailPath);
            }
            catch
            {
                EditorUtility.DisplayDialog("Upload Failed", "Failed to Upload. Please see the console tab and check the error.\n\n"
                + "アップロードに失敗しました。Consoleタブを確認して、エラーを確認してください。", "OK");
                throw;
            }
        EditorUtility.DisplayDialog("Upload Successful", "Upload Succeeded!\nTarget avatar: \"" + targetAvatar.name + "\"\n\n"
        + "アップロードが完了しました！\n対象アバター: \"" + targetAvatar.name + "\"", "OK");
        }
    }

    internal class AvatarInfo : VRCSdkControlPanelAvatarBuilder
    {
        public static GameObject GetSelectedAvatarInControlPanel()
        {
            var selectedAvatar = VRCSdkControlPanelAvatarBuilder._selectedAvatar;
            return selectedAvatar != null ? selectedAvatar.gameObject : null;
        }
    }
}
