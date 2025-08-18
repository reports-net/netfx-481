# 📖 Reports.net WEB アプリケーション

※Visual Studio 2022では、Shift+F7 でこのREADME.md をプレビュー表示してください。

※Visual Studio 2019以前ではMarkdownプレビュー機能はありません。  
※プレビューが必要な場合は、Visual Studio Code等の外部ツールをご利用ください。

<br>

## 🌟 概要

Reports.net WEB アプリケーションは、ブラウザ上で直接帳票を作成・表示できる Web アプリケーションです。

## クイックスタート

1. **ソリューションを開く**:
   - `Reports.net.Sample.WebSite.sln`

2. **実行**:
   - F5キーまたは実行ボタンをクリック
   - 自動的にブラウザが起動し、アプリケーションが表示されます

3. **帳票の作成・表示**:
   - 画面の指示に従って帳票を作成
   - ブラウザ上で直接プレビューまたはダウンロード


**🔗 デモサイト**: http://reportssamplewebsite.azurewebsites.net/

## 📋 対象バージョン

- **✅ 対象**: .NET Framework 用のAzure発行参考プログラム
- **❌ 対象外**: .NET 5 ～ .NET 9以降

> **💡 .NET 5以降をお使いの方へ**  
> 適合したインストーラをダウンロードしてください。「.NET 5 ～ .NET 9以降 用のWEBアプリケーション」サンプルプログラムもご用意しています。Linux対応版（AWS/Azure/Docker対応）も利用可能です。

## 🗄️ データベース接続

- **DB**: SQL Server（Azure上）
- **接続**: 読み込み専用ユーザーで接続する接続文字列を使用

## 🚀 実行環境

| 環境 | 動作 | 備考 |
|------|------|------|
| ローカルデバッグ | ✅ 動作 | - |
| Azureへのデプロイ | ❌ 不可 | 設定値を変更してご利用ください |

## 📺 学習リソース

Reports.netの様々な使い方について、動画やブログ記事をご用意しています。

**🎬 動画チャンネル**  
https://www.youtube.com/channel/UCYKjmyVrFhW_w_WhLU-wdqA?sub_confirmation=1

**📝 コンテンツ例**
- Linux対応WEBアプリケーション
- Reports.netを使った帳票出力WEB API（REST）
- Docker/AWS/Azure/GCP対応手順

---

## 🔧 トラブルシューティング

### 🚨 プロジェクトが読み込めない・起動できない場合

#### 🥇 **最優先対応**: OneDriveによるIIS Expressエラー

**📅 発生時期**: 2025年5月現在の主要原因（Windows Update後に多発）

**🔍 症状**
- ソリューション(.sln)を開くとプロジェクトが灰色表示
- `redirection.config`の構成ファイル読み取りエラー

**💡 原因**  
OneDriveがDocumentsフォルダーを管理している場合、IIS Expressの設定ファイルが「オンラインのみ」状態になってしまう問題

**✅ 解決手順**
1. エクスプローラーを開く
2. 以下のパスを確認
   - `C:\Users\[ユーザー名]\OneDrive\Documents\IISExpress`
   - `C:\Users\[ユーザー名]\Documents\IISExpress`
3. `C:\Users\[ユーザー名]\OneDrive\Documents\IISExpress`フォルダーを右クリック
4. 「このデバイス上に常に保持する」を選択
5. パソコンを再起動

---

### 🚨 その他のエラー

#### Microsoft.Web.Infrastructure エラー

**🔍 症状**
```
ファイルまたはアセンブリ 'Microsoft.Web.Infrastructure, Version=1.0.0.0, 
Culture=neutral, PublicKeyToken=31bf3856ad364e35'、またはその依存関係の 1 つが
読み込めませんでした。指定されたファイルが見つかりません。
```

**✅ 解決手順**

NuGetパッケージを以下の順序で再インストール：

**🗑️ アンインストール順序**
1. Microsoft.AspNet.Web.Optimization.WebForms
2. Microsoft.AspNet.Web.Optimization
3. Microsoft.Web.Infrastructure

**📦 インストール順序**
1. Microsoft.Web.Infrastructure
2. Microsoft.AspNet.Web.Optimization
3. Microsoft.AspNet.Web.Optimization.WebForms

> **📌 注意**: 全てのバージョンを `1.0.0` に統一してください

---

#### ポート使用中エラー

**🔍 症状**
```
localhost により、接続が拒否されました。
指定されたポートは使用中です。
ポート 'nnnnn' はその他のアプリケーションで既に使用されています。
```

**✅ 解決手順**

1. **📁 .vsフォルダを削除**

2. **⚙️ .csprojファイルのポート設定を変更**

   **IISExpressSSLPort の設定**
   ```xml
   <!-- 変更前 -->
   <IISExpressSSLPort />
   
   <!-- 変更後（53333は例） -->
   <IISExpressSSLPort>53333</IISExpressSSLPort>
   ```

   **WebProjectProperties の設定**
   ```xml
   <!-- ポート番号を統一（例：53333） -->
   <DevelopmentServerPort>53333</DevelopmentServerPort>
   <IISUrl>http://localhost:53333/</IISUrl>
   ```

> **🔗 参考情報**  
> 詳細な解決方法は以下のサイトもご参照ください：  
> https://www.ipentec.com/document/visual-studio-error-iis-express-the-specified-port-is-in-use-when-starting-debug

**🎉 完了**  
変更後、ソリューションファイルを再度開いてプロジェクトを実行すると、エラーなく動作します。

---

> 📝 **Note**: このサンプルは**WEB アプリケーション**用です。WEB WPI サンプルとは異なりますのでご注意ください。

----

© Reports.net - クラウド対応帳票出力ソリューション