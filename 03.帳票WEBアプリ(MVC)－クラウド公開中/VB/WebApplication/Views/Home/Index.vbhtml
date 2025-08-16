﻿@Code
    ViewData("Title") = "Reports.net .NET Framework Web Application"
End Code


<div class="text-center">
    <!--
    <h1>.NET Framewrok on Azure Winows Server の IIS</h1><h4 class="display-5">Reports.net WEB APP Sample</h4>
    -->
    <h1>.NET Framework ローカル開発環境の IIS </h1><h4 class="display-5">Reports.net WEB APP Sample</h4>
    <p>
        <a href="http://www.pao.ac/">Product Site</a>.　<a href="https://www.youtube.com/channel/UCYKjmyVrFhW_w_WhLU-wdqA">Youtube Channel</a>.　<a href="https://pao-at-office.hatenablog.com/archive">blog</a>.
    </p>
    <br />
</div>

<div>
    <form method="post">
        <table align="center">
            <tr>
                <td align="center">
                    <input type="radio" name="ReportsKind" value="simple" checked="checked" />単純なサンプル　
                    <input type="radio" name="ReportsKind" value="simple10" />10の倍数　
                    <input type="radio" name="ReportsKind" value="zipcode" />郵便番号　
                    <input type="radio" name="ReportsKind" value="mitsumori" />見積書　
                    <input type="radio" name="ReportsKind" value="invoice" />請求書　
                    <input type="radio" name="ReportsKind" value="itemlist" />商品一覧　
                    <input type="radio" name="ReportsKind" value="koukoku" />広告 　
                </td>
            </tr>
            <tr>
                <td align="center">
                    <br />
                    <br />
                    <input type="submit" name="PdfAction" value="View PDF" formtarget="_blank" />
                    <input type="submit" name="PdfAction" value="Download PDF" />
                </td>
            </tr>
        </table>
    </form>

    <hr />
    <div>

        <!--
        <h5>各環境で現在動作しているサンプルWEBアプリケーション (全てこのプログラムから発行しています)</h5>
        -->
        <h5>外部クラウド環境で現在動作しているサンプルWEBアプリケーション</h5>
        (URLが変更になる可能性があるためリダイレクトしております。)
        <ul>
            <li><a href="http://www.pao.ac/reports.net/redirect-azure-aws-gcp/azure-win.html">Azure - Windows Server</a></li>
            <li><a href="http://www.pao.ac/reports.net/redirect-azure-aws-gcp/azure-linux.html">Azure - Linux (CentOS)</a></li>
            <li><a href="http://www.pao.ac/reports.net/redirect-azure-aws-gcp/aws-folder.html">AWS - EC2 - Amazon Linux 2 - folderデプロイ方式</a></li>
            <li><a href="http://www.pao.ac/reports.net/redirect-azure-aws-gcp/aws-docker.html">AWS - EC2 - Amazon Linux 2 - docker方式</a></li>
            <li><a href="http://www.pao.ac/reports.net/redirect-azure-aws-gcp/gcp-docker.html">GCP - GCE - Debian GNU/Linux - docker方式</a></li>
        </ul>
        ※各クラウド環境で動作しているWEBアプリケーションについては、事情により動作を停止させていただく場合がございます。

    </div>
    <hr />

    <div>
        <div class="setsumei">
            Reports.net が、LinuxのWEBアプリケーションからPDF出力できるようになりました。これは、そのサンプルWEBアプリケーションです。<br />
            このWEBアプリケーションを基にAWSやAzure,GCP,オンプレといった様々な環境や構成や手法(例えばDockerを使用する・しない等)で、デプロイして動作を実現している動画や解説ブログを作成してございます。<br />
            動画サイトとブログサイトでは、多くの用途にお応えするために具体的に様々な環境・構成の手順を動画・記事としてまとめてあります。是非、ご覧になってください。
            <ul>
                <li>
                    <h4><a href="https://www.youtube.com/channel/UCYKjmyVrFhW_w_WhLU-wdqA?sub_confirmation=1">動画サイト</a></h4>
                    (常時動画を追加してまいりますので、是非チャンネル登録をお願いいたします。)
                    <br />
                    <br /><a href="https://youtu.be/I0XQq4VYO7U">Reports.netで帳票作成・帳票出力－簡単な利用方法</a><br />
                    <br />
                    <h5>WEBアプリケーション 動画</h5>
                    <ol>

                        <li><a href="https://youtu.be/6UI_pP-ws3c">WEBアプリでPDF帳票出力開発手法紹介 / AzureへのWEBアプリをデプロイ手順 / Azure SQL Server使用</a></li>
                        <li><a href="https://youtu.be/OF3y7875BGo">Linux上で動作する.NET5/6(C#)-帳票出力WEBアプリ作成手順 - [WSL2 & Azure-Linux編] </a></li>
                        <li><a href="https://youtu.be/1wTuV2ffATg">帳票出力WEBアプリをAWS Linux へデプロイ - AWS Toolkit for Visual Studio使用</a></li>
                        <li><a href="https://youtu.be/0y3K3CW7DRM">.NET5 Dockerの最も単純な方法でAWS-EC2上で帳票出力WEBアプリを動作させる手順</a></li>
                        <li><a href="https://youtu.be/UnPXcadLwFY">.NET6 Dockerの最も単純な方法でAWS-EC2上で帳票出力WEBアプリを動作させる手順</a></li>
                        <li><a href="https://youtu.be/TQpeQGwGNmM">AWS ECS/ECRで帳票出力WEBアプリをDockerで動作させる手順</a></li>
                        <li><a href="https://youtu.be/YFdjUg9KgFo">Dockerの最も単純な方法でGCP上で帳票出力WEBアプリを動作させる手順</a></li>
                        <li><a href="https://youtu.be/igApoNMri7k">帳票出力WEBアプリを複数クラウドにマルチデプロイ - Azure / AWS / GCP</a></li>
                        <li><a href="https://youtu.be/3SE7hLNcOo8">超簡単、帳票出力WEBアプリをフォルダデプロイ方式で AWS EC2へ</a></li>
                    </ol>
                    <br />
                    <h5>WEB API 動画</h5>
                    <ol>
                        <li><a href="https://youtu.be/cYEtHFpa8G4">最短/最速 REST API(WEB API)実装 GET編 / IIS+Windows Formクライアント編 / .NET5/6(.NET Framework 4.xも可</a></li>
                        <li><a href="https://youtu.be/EflMRmMYU4A">最短/最速 REST API(WEB API)実装 POST編 / IIS+Windows Formクライアント編 / .NET5/6(.NET Framework 4.xも可</a></li>
                        <li><a href="https://youtu.be/xHNLlPuMFEs">Visual Studio から WEB API を IISへデプロイ手順。.NET 5/.NET Framework 4.5 / 意外と難しいんですよ。</a></li>
                        <li><a href="https://youtu.be/Bolfww56aWY"> [.NET5/6] REST API(WEB API)で帳票作成 / Windows Form クライアントで帳票出力</a></li>
                        <li><a href="https://youtu.be/VNeD7w3LdV0"> [.NET5/6] REST API(WEB API)で帳票作成-2 SQL Server編/Windows Form クライアントで出力 / 「実践技」盛りだくさん</a></li>
                        <li><a href="https://youtu.be/KW_RK8PmXro">帳票出力WEB APIを複数クラウドにマルチデプロイ - Azure / AWS / GCP</a></li>
                    </ol>

                    <br />
                <li>
                    <h4><a href="https://pao-at-office.hatenablog.com/archive">ブログサイト</a></h4>
                </li>
            </ul>
            <br />
            ※動画サイト・ブログサイトには、WEBアプリケーションだけでなく、Reports.netを利用した帳票出力WEB API(REST)の作成方法や様々な環境へのデプロイ手順など掲載してございます。是非、ご覧ください。
        </div>
    </div>

    <div>
        @TempData["debug"]
        <br />
        @TempData["debug2"]
    </div>

</div>
