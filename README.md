## Upload without pre-check
Add a menuitem to Upload an avatar without pre-check.

### Why does this tool exist?
See below.
- https://github.com/bdunderscore/ndmf/issues/88
- https://feedback.vrchat.com/sdk-bug-reports/p/feedback-please-provide-way-for-official-control-panel-to-avoid-pre-check

If NDMF or VRCSDK adds similar feature, this package will be deprecated.

---
---
## 日本語説明
VRCSDKのビルド前チェックをスキップしてアバターをアップロードするためのメニュー項目を追加するツールです。

Android向けにビルドしたい場合などにおいて、AAO等の非破壊改変ツールによってPB等のHard Limitを下回るにも関わらず、ビルド前チェックの時点でHard Limitに引っ掛かっているためにアップロード出来ない問題を解決します。\
なお、VRCSDKにはビルド後チェック(アップロード直前のチェック)の仕組みもあるため、制限を超過している場合にはアップロードに失敗します。

### なぜこのツールが存在するのですか?
以下をご覧ください。
- https://github.com/bdunderscore/ndmf/issues/88
- https://feedback.vrchat.com/sdk-bug-reports/p/feedback-please-provide-way-for-official-control-panel-to-avoid-pre-check

NDMFかVRCSDKに同様の機能が追加された場合、このパッケージは非推奨になります。