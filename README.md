# Upload without pre-check
## 日本語説明
VRCSDKのビルド前チェックをスキップしてアバターをアップロードするためのメニュー項目を追加するツールです。

Android向けにビルドしたい場合などにおいて、AAO等の非破壊改変ツールによってPB等のHard Limitを下回るにも関わらず、ビルド前チェックの時点でHard Limitに引っ掛かっているためにアップロード出来ない問題を解決します。\
なお、VRCSDKにはビルド後チェック(アップロード直前のチェック)の仕組みもあるため、制限を超過している場合にはアップロードに失敗します。

### 導入方法
(VPMリポジトリが追加済みの場合、VCCからそのまま追加出来ます。)
1. https://sayabeans.github.io/vpm にアクセスします。
2. "Add to VCC"をクリックしてVCCに追加します。
3. VCCからパッケージを追加します。

### 使い方
1. VRCSDKのコントロールパネルを開きます
2. (新しいアバターをアップロードする場合は、コントロールパネルで対象のアバターを選択して情報を入力します)
3. ヒエラルキーでアバターを選択します
4. アバターのオブジェクトを右クリックしてメニューを開きます
5. `[VRChat] Upload without pre-check`をクリックします

### なぜこのツールが存在するのですか?
以下をご覧ください。
- https://github.com/bdunderscore/ndmf/issues/88
- https://feedback.vrchat.com/sdk-bug-reports/p/feedback-please-provide-way-for-official-control-panel-to-avoid-pre-check

NDMFかVRCSDKに同様の機能が追加された場合、このパッケージは非推奨になります。

---
---
## English Description
Add a menuitem to Upload an avatar without pre-check.

### How To Use
1. Open the VRCSDK Control Panel
2. (If you want to upload a new avatar, select it on the Control Panel and enter the avatar information)
3. Select your avatar in Hierarchy
4. Right-click the avatar GameObject to open menu
5. Click `[VRChat] Upload without pre-check`

### Why does this tool exist?
See below.
- https://github.com/bdunderscore/ndmf/issues/88
- https://feedback.vrchat.com/sdk-bug-reports/p/feedback-please-provide-way-for-official-control-panel-to-avoid-pre-check

If NDMF or VRCSDK adds similar feature, this package will be deprecated.
