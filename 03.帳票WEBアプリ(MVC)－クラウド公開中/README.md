※Visual Studio 2026では、このREADME.md が自動的にプレビュー表示されます。
※Visual Studio 2022では、Shift+F7 でプレビュー表示してください。

※Visual Studio 2019以前ではMarkdownプレビュー機能はありません。
※プレビューが必要な場合は、Visual Studio Code等の外部ツールをご利用ください。
<br>

# 帳票WEBアプリケーション

WEB アプリケーションで帳票を出力するなら、PDF。
長い間、それが唯一の選択肢でした。

このサンプルは、その常識を書き換えます。

PDF に加えて **SVG** という新しい選択肢を用意し、
ブラウザの中だけで帳票のプレビューと印刷を完結させます。
PDF ビューアのプラグインも、外部アプリケーションも必要ありません。

---

## PDF では実現できなかったもの

PDF は帳票の配布には最適です。どの環境でも同じ見た目で表示される、その信頼性は揺るぎません。

しかし、ブラウザ上での「プレビュー」となると話が変わります。
PDF をブラウザで表示するには、ブラウザ内蔵の PDF ビューアか、外部プラグインに依存します。
ページ送りの操作感は PDF ビューア次第。印刷も PDF ビューアの機能を通すことになります。
WEB アプリケーション側からは、表示も操作もほとんどコントロールできません。

SVG なら、状況は一変します。

SVG はブラウザがネイティブに描画できるベクター画像です。
HTML の一部として埋め込めば、ブラウザが直接レンダリングします。
JavaScript でページを切り替え、CSS でスタイルを制御し、
`window.print()` で印刷ダイアログを呼び出す。
WEB アプリケーションが帳票の表示と印刷を完全にコントロールできるのです。

---

## 2つの SVG モード

このサンプルには、SVG による帳票表示の2つのアプローチが実装されています。

### SVG 一括 — `GetSvg()`

全ページの SVG を一つの HTML にまとめて返します。

```csharp
// コントローラー: 全ページを一括取得
string html = report.GetSvg();
return Content(html, "text/html", Encoding.UTF8);
```

ブラウザには全ページが縦に並んで表示されます。
スクロールするだけで全ページが閲覧でき、
そのまま「View」で表示、「Download」で HTML ファイルとして保存できます。

### SVG プレビュー — `GetSvgTag()`

1ページ分の SVG タグだけを返します。ブラウザ上でページ送りの UI を構築できます。

```csharp
// コントローラー: 全ページのSVGタグを取得してビューに渡す
List<string> svgPages = new List<string>();
for (int i = 1; i <= report.AllPages; i++)
{
    svgPages.Add(report.GetSvgTag(i));
}
ViewBag.SvgPages = svgPages;
return View("ShowSvgPreview");
```

```javascript
// ShowSvgPreview.cshtml: ページ送り（クライアントサイド）
var svgPages = @Html.Raw(Json.Encode(ViewBag.SvgPages));

function loadPage(page) {
    document.getElementById('svgContainer').innerHTML = svgPages[page - 1];
}
```

前へ、次へ、先頭へ、末尾へ。ページ番号を直接入力してジャンプ。
クイック印刷で現在のページだけを印刷。通常印刷で全ページを一括印刷。
PDF ビューアに頼ることなく、ブラウザの中だけで完結する印刷プレビューです。

`GetSvg()` が全体把握に向いているのに対し、
`GetSvgTag()` は1ページずつ丁寧に確認する用途に向いています。
Reports.net がこの2つのメソッドを用意したことで、
WEB アプリケーションの帳票プレビューに、PDF とは異なる新しい道が開かれました。

---

## 3つの出力形式

画面のラジオボタンで出力形式を選択します。

| 形式 | メソッド | 動作 |
|------|----------|------|
| **PDF** | `GetPdf()` + `SavePDF()` | ブラウザの PDF ビューアで表示 / ファイルダウンロード |
| **SVG 一括** | `GetSvg()` | 全ページの SVG を含む HTML を表示 / ダウンロード |
| **SVG プレビュー** | `GetSvgTag(page)` | ページ送り付きのインタラクティブプレビュー |

PDF も SVG も、帳票生成のロジックは同一です。

```csharp
// SVG の場合
IReport report = ReportCreator.GetReport();
Make見積書(report);  // 帳票データの流し込みは同じ

// PDF の場合
IReport report = ReportCreator.GetPdf();
Make見積書(report);  // まったく同じコード
```

`ReportCreator.GetReport()` か `ReportCreator.GetPdf()` か。
インスタンスの生成方法が違うだけで、帳票にデータを書き込むコードは一切変わりません。

---

## ここから始まった

このWEBアプリケーションで実現した「ブラウザ + SVG による帳票プレビュー」というアイデアは、
その後、デスクトップとモバイルに展開されていきました。

| サンプル | 方式 | 着想 |
|----------|------|------|
| **06.WPFでSVGプレビューと印刷** | WebView2 + `GetSvgTag()` | ブラウザで動くなら WebView2 でも動く |
| **07.MAUIでSVGプレビューと印刷** | WebView + `GetSvgTag()` | GDI+ のない MAUI でも SVG なら動く |
| **08.スマホやiPhoneで印刷とプレビュー** | WebView + `GetSvg()` | スマホでもブラウザエンジンは同じ |

ブラウザが SVG を描画できる。ブラウザの印刷機能で帳票を印刷できる。
この単純な事実から出発して、WPF の WebView2 へ、MAUI の WebView へ、
そしてスマートフォンの Android / iOS へと、同じ仕組みが広がっていきました。

すべては、この WEB アプリケーションが出発点です。

---

## 7種類の帳票サンプル

ラジオボタンから帳票を選択し、出力形式を選んで「View」または「Download」をクリックします。

| 帳票 | 内容 | 注目ポイント |
|------|------|-------------|
| **単純なサンプル** | 基本的な帳票出力 | Write メソッドの基本操作 |
| **10の倍数** | 複数ページにまたがる帳票 | SVG プレビューでのページ送り動作 |
| **郵便番号一覧** | SQL Server からの一覧帳票 | QRコード、交互背景色 |
| **見積書** | ヘッダ＋明細のビジネス帳票 | 表紙と本体で定義ファイルを分離 |
| **請求書** | 動的テーブル生成 | 罫線スタイル、交互色、小計・税計算 |
| **商品一覧** | 大分類・小分類付きリスト | カテゴリ集計、小計行の色分け |
| **広告** | 画像・QRコード入り広告チラシ | 画像配置、バーコード生成 |

> **データソース**: 単純なサンプルと10の倍数はローカルのみ。
> 郵便番号以降の帳票は Azure SQL Server からデータを取得します。

---

## 使い方

1. `CS\WebApplication.sln` を Visual Studio で開く
2. NuGet パッケージの復元を実行
3. F5 で実行
4. 帳票と出力形式を選んで「View」をクリック
5. SVG プレビューでページ送り、印刷を試す

---

## 動作環境

- .NET Framework 4.7.2
- Windows 10 / 11（IIS Express）
- Reports.net（Pao.Reports.Azure.dll）
- Visual Studio 2017 以降
- Azure SQL Server への接続（郵便番号以降の帳票）

---

## ファイル構成

```
03.帳票WEBアプリ(MVC)/
├── CS/
│   ├── WebApplication.sln         ソリューションファイル
│   └── WebApplication/
│       ├── WebApplication.csproj   .NET Framework 4.7.2 MVC プロジェクト
│       ├── Global.asax.cs          アプリケーション起動
│       ├── Controllers/
│       │   └── HomeController.cs   帳票生成・SVG/PDF 出力ロジック
│       ├── Views/Home/
│       │   ├── Index.cshtml        メイン画面（帳票選択・形式選択）
│       │   ├── ShowSvgPreview.cshtml SVG ページ送りプレビュー
│       │   └── ShowPdf.cshtml      PDF 表示（iframe）
│       ├── App_Data/               帳票定義ファイル(.prepd)・画像
│       ├── App_Start/              ルーティング・バンドル設定
│       └── Content/Scripts/        Bootstrap・jQuery
└── VB/                             Visual Basic 版
```

---

## 技術メモ

### SVG プレビューのページ送り

.NET_ALL 版（ASP.NET Core）ではページごとに AJAX で取得する方式ですが、
この .NET Framework 版では、初期表示時に全ページの SVG を JavaScript 配列として渡しています。

```javascript
// 全ページのSVGを一括でクライアントに渡す
var svgPages = @Html.Raw(Json.Encode(ViewBag.SvgPages));

// ページ送りはクライアントサイドで完結（サーバーへの追加リクエストなし）
function loadPage(page) {
    document.getElementById('svgContainer').innerHTML = svgPages[page - 1];
}
```

この方式の利点は、ページ送りが瞬時に行われることです。
サーバーとの通信が不要なため、ネットワーク遅延の影響を受けません。

### 印刷

```javascript
// 現在のページだけ印刷
window.print();

// 全ページ印刷: 新しいウィンドウに全SVGを展開して印刷
var w = window.open('', '_blank');
w.document.write('<style>svg { page-break-after: always; }</style>');
w.document.write(svgPages.join(''));
w.print();
```

全ページ印刷では、すでにクライアントにある SVG データを
`page-break-after` 付きで新しいウィンドウに展開し、`window.print()` を実行します。
サーバーへの追加リクエストは発生しません。

### クラウドで動作中

このサンプルは Azure Windows Server 上で公開動作しています。
Index.cshtml の「外部クラウド環境」リンクから、
クラウド上のインスタンスにアクセスして SVG プレビューの動作を確認できます。

---

## .NET 8 版との対応

このサンプルは `.NET_ALL/03.帳票WEBアプリケーション` の .NET Framework 4.7.2 移植版です。

主な違い:

| | .NET_ALL 版（ASP.NET Core） | VS2013-VS2017 版（ASP.NET MVC 5） |
|---|---|---|
| フレームワーク | .NET 8 | .NET Framework 4.7.2 |
| SVG プレビュー | ページごとに AJAX 取得 | 初期ロード時に全ページ一括取得 |
| Docker 対応 | あり（Dockerfile 同梱） | なし |
| Linux 動作 | PaoWebApp.sln で対応 | Windows のみ |

SVG プレビューの操作感と帳票生成のロジックは同一です。
.NET 8 以降の環境をお使いの場合は `.NET_ALL/03` をご利用ください。

---

## トラブルシューティング

### OneDrive による IIS Express エラー

**症状**
- ソリューション(.sln)を開くとプロジェクトが「アンロード済み」として灰色表示される
- エラー: 構成ファイル `redirection.config` を読み取れません

**原因**
OneDrive が Documents フォルダーを管理している場合、IIS Express の設定ファイルが「オンラインのみ」状態になり、ローカルで利用できなくなることがあります。Windows Update 後に発生しやすい問題です。

**解決方法**
1. エクスプローラーを開く
2. 以下のパスを確認:
   - `C:\Users\[ユーザー名]\OneDrive\Documents\IISExpress`
   - `C:\Users\[ユーザー名]\Documents\IISExpress`
3. OneDrive 配下の `IISExpress` フォルダーを右クリック
4. 「このデバイス上に常に保持する」を選択
5. パソコンを再起動

---

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
