※Visual Studio 2022では、Shift+F7 でこのREADME.md をプレビュー表示してください。

※Visual Studio 2019以前ではMarkdownプレビュー機能はありません。  
※プレビューが必要な場合は、Visual Studio Code等の外部ツールをご利用ください。

<br>
# Reports.net WEB API サンプルアプリケーション

## フォルダとソリューション構成

```
04.帳票Web API(クラウド/オンプレミス連携)
  ├── WebApi.sln      - WEB API + クライアント統合
  ├── README.md               - このファイル
  │
  ├── Client/                 - 帳票出力クライアントプロジェクト
  │    ├── ClientForm/        - Windows Form版クライアント
  │    └── ClientWpf/         - WPF版クライアント
  │
  └── WebApi/                 - 帳票作成 WEB API プロジェクト
       └── App_Data/          - コントローラで利用する帳票デザイン・画像
 ```

## クイックスタート

### 🚀 ローカル開発環境での実行（推奨）

1. **ソリューションを選択して起動**:
   - `WebApi.sln` を開く
   
2. **F5キーで実行**:
   - **Visual Studio 2019/2022**: **3つのプロジェクトが同時に起動**されます
   - **Visual Studio 2017以前**: 各プロジェクトを個別に起動してください

> **Visual Studio 2017以前をご利用の場合**
> 
> マルチスタートアップ機能がないため、以下の順序で個別に起動してください：
> 1. WebApi プロジェクトを右クリック → 「デバッグ」→「新しいインスタンスを開始」
> 2. ClientForm プロジェクトを右クリック → 「デバッグ」→「新しいインスタンスを開始」  
> 3. ClientWPF プロジェクトを右クリック → 「デバッグ」→「新しいインスタンスを開始」

3. **帳票出力**:
   - Windows Form/WPF いずれかお好みのクライアントを選択
   - 「ローカルデバッグ環境」を選択
   - 「実行 (POST)」または「実行 (GET)」ボタンをクリック

### ☁️ クラウド環境利用

1. **クライアントのみ起動**:
   - 上記の手順でクライアントが起動したら、クラウド環境を選択：
     - ローカルデバッグ環境
     - Azure Windows Server

2. **帳票出力**:
   - 「実行 (POST)」または「実行 (GET)」ボタンをクリック

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

## 処理の流れ

1. クライアント（Windows Form/WPF）→(印刷データ作成依頼)→サーバサイド(WEB API)
2. サーバサイド(WEB API)→(印刷データ)→クライアント→帳票出力
という流れになります。

このソリューションには、WEB APIサーバーと2種類のクライアント（Windows Form版・WPF版）が含まれています。

デフォルトでは、ソリューションのマルチプルスタートアップ設定により、F5キーを押すことで
「WEB API サーバ」「Windows Form版クライアント」「WPF版クライアント」の3つが同時に起動されます。
Windows Form/WPF いずれかお好みのクライアントをご利用ください。

※WEB APIプロジェクトが先に起動し、ブラウザで「ASP.NET」が表示されます。

プログラム起動後は、WEB APIの配置場所を選択してください：
[ローカルデバッグ環境 / Azure Windows]

配置場所と「出力帳票の種類」を選択後、「実行 (POST)」「実行 (GET)」ボタンをクリックすると
WEB API から印刷データを取得し、帳票出力を行います。

※WEB APIソリューションはWindows版のみご用意しています。Linux版は、.NET5以降をインストールしてご利用ください。

```
Windows Forms/WPF
クライアント ──[印刷データ作成依頼]─→ WEB API
     ↓                                 │
【帳票出力】 ←──────[印刷データ]─────────┘
```

## Windows版とLinux版のWEB APIの違い (Linux版は、.NET5以降でご利用いただけます)

| 機能・特性 | Windows版 | Linux版 |
|------------|-----------|---------|
| 参照DLL    | Pao.Reports.Azure.dll | Pao.Reports.Linux.dll |
| Windows上での動作 | ✅ | ✅ |
| Linux上での動作   | ❌ | ✅ |
| イメージPDF出力   | ✅ | ❌ |

> 📝 **Note**: Windows版がLinux上で動作しない理由は、System.Drawingの依存関係によるものです。

## よくある質問 (FAQ)

**Q: 3つのプロジェクトが同時に起動しますが、どれを使えばよいですか？**  
A: WEB API（ASP.NET MVC）は開発用インターフェースです。実際の帳票出力には Windows Form または WPF のいずれかお好みのクライアントをご利用ください。

**Q: サーバーサイドのWEB APIを起動するとブラウザが開きますが、何をすればよいですか？**  
A: これはASP.NET MVCという開発用インターフェースです。特に操作は不要で、クライアントアプリから利用されます。

**Q: クラウド環境のAPIにアクセスできない場合は？**  
A: クラウド環境は予告なく停止する場合があります。その場合は、ローカル環境でサーバーを実行してください。

**Q: プロジェクトが起動しない場合は？**  
A: 上記の「トラブルシューティング」セクションをご確認ください。OneDriveによるIIS Express問題が最も多い原因です。

**Q: ローカル環境で接続エラーが発生します**  
A: WEB APIサーバーが起動していることを確認してください。マルチプルスタートアップ設定により、通常は自動的に起動されます。

## チュートリアル動画

1. [最短/最速 REST API実装 GET編](https://youtu.be/cYEtHFpa8G4)
2. [最短/最速 REST API実装 POST編](https://youtu.be/EflMRmMYU4A)
3. [Visual Studio から IISへデプロイ手順](https://youtu.be/xHNLlPuMFEs)
4. [REST APIで帳票作成](https://youtu.be/Bolfww56aWY)
5. [REST APIで帳票作成 SQL Server編](https://youtu.be/VNeD7w3LdV0)
6. [複数クラウドにマルチデプロイ](https://youtu.be/KW_RK8PmXro)

---

> 📝 **Note**: このサンプルは**WEB API**用です。WEBアプリケーション サンプルとは異なりますのでご注意ください。

----

© Reports.net - クラウド対応帳票出力ソリューション