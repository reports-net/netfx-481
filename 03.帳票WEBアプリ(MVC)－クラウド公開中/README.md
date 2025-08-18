※Visual Studio 2022では、Shift+F7 でこのREADME.md をプレビュー表示してください。

※Visual Studio 2019以前ではMarkdownプレビュー機能はありません。  
※プレビューが必要な場合は、Visual Studio Code等の外部ツールをご利用ください。
<br>
# Reports.net WEB アプリケーション

## 概要

Reports.net WEB アプリケーションは、ブラウザ上で直接帳票を作成・表示できる Web アプリケーションです。


## クイックスタート

1. **ソリューションを開く**:
   - `PaoWebApp.sln` (Linux互換版) または
   - `PaoWebApp.Windows.sln` (Windows専用版)

2. **実行**:
   - F5キーまたは実行ボタンをクリック
   - 自動的にブラウザが起動し、アプリケーションが表示されます

3. **帳票の作成・表示**:
   - 画面の指示に従って帳票を作成
   - ブラウザ上で直接プレビューまたはダウンロード

**🌐 動作確認:**
```
まずは ブラウザで http://localhost:44385
```
Reports.net WEBアプリケーションのホーム画面が表示され、帳票作成機能が利用できます。

---

## 🚨 トラブルシューティング

### プロジェクトが読み込めない・起動できない場合

#### A. 【最優先】OneDriveによるIIS Express エラー（2025年現在の主要原因）

**🎯 症状**
- ソリューション(.sln)を開くとプロジェクトが「アンロード済み」として灰色表示される
- プロジェクトを直接開くとエラー発生：
  - ファイル名: redirection.config
  - エラー: 構成ファイルを読み取れません

**🔍 原因**  
OneDriveがDocumentsフォルダーを管理している場合、IIS Expressの設定ファイルが「オンラインのみ」状態になり、ローカルで利用できなくなることがある。Windows Update後に発生しやすい問題です。

> ⚠️ **重要**: .NET 5以降でもVisual Studioのデバッグ実行時はIIS Expressが使用されるため、同様の問題が発生します。

**✅ 解決方法（推奨⭐）**
1. エクスプローラーを開く
2. 以下のパスを確認：
   - `C:\Users\[ユーザー名]\OneDrive\Documents\IISExpress`
   - `C:\Users\[ユーザー名]\Documents\IISExpress`
3. `C:\Users\[ユーザー名]\OneDrive\Documents\IISExpress`フォルダーを右クリック
4. 「このデバイス上に常に保持する」を選択
5. パソコンを再起動

---

## Linux版とWindows版の違い (Linux版は、.NET5以降でご利用いただけます)

| 機能・特性 | Windows版 | Linux版 |
|------------|-----------|---------|
| 参照DLL    | Pao.Reports.Azure.dll | Pao.Reports.Linux.dll |
| Windows上での動作 | ✅ | ✅ |
| Linux上での動作   | ❌ | ✅ |
| イメージPDF出力   | ✅ | ❌ |

> 📝 **Note**: Windows版を選択する主な理由は「イメージPDF出力」機能が必要な場合です。それ以外の用途では、より互換性の高いLinux版を推奨します。

## クラウドデプロイ

このWebアプリケーションは、各種クラウドWondowsサーバ環境へのデプロイに対応しています：

## よくある質問 (FAQ)

**Q: クラウドにデプロイするための推奨方法は？**  
A: 簡易的には、Visual StudioのAzureへの「発行」で簡単にデプロイして動作いたします。

**Q: プロジェクトが起動しない場合は？**  
A: 上記の「トラブルシューティング」セクションをご確認ください。OneDriveによるIIS Express問題が最も多い原因です。

## チュートリアル動画

### Docker デプロイ
1. [.NET5 Docker - AWS-EC2上で帳票出力WEBアプリを動作させる手順](https://youtu.be/0y3K3CW7DRM)
2. [.NET6 Docker - AWS-EC2上で帳票出力WEBアプリを動作させる手順](https://youtu.be/UnPXcadLwFY)
3. [AWS ECS/ECRで帳票出力WEBアプリをDockerで動作させる手順](https://youtu.be/TQpeQGwGNmM)
4. [GCP上で帳票出力WEBアプリをDockerで動作させる手順](https://youtu.be/YFdjUg9KgFo)
5. [複数クラウドにマルチデプロイ - Azure / AWS / GCP](https://youtu.be/igApoNMri7k)

### その他のデプロイ方法
1. [AzureへのWEBアプリをデプロイ手順 / Azure SQL Server使用](https://youtu.be/6UI_pP-ws3c)
2. [Linux上で動作する帳票出力WEBアプリ作成手順 - WSL2 & Azure-Linux編](https://youtu.be/OF3y7875BGo)
3. [AWS Linux Elastic Beanstalkへデプロイ - DynamoDB使用](https://youtu.be/1wTuV2ffATg)
4. [フォルダデプロイ方式で AWS EC2へ](https://youtu.be/3SE7hLNcOo8)

## アーキテクチャ概要

```
ブラウザ ───┬──[HTTP/HTTPS]──→ WEB アプリケーション
            │                    │ 
            │                    ├─[帳票データ処理]
            │                    │ 
            └──[帳票表示]←───────┘
               (PDF/SVG/XPS)
```

---

> 📝 **Note**: このサンプルは**WEBアプリケーション**用です。WEB API サンプルとは異なりますのでご注意ください。

----

© Reports.net - クラウド対応帳票出力ソリューション