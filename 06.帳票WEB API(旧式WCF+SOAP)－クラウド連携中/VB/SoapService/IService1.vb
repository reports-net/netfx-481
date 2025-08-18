' メモ: コンテキスト メニューの [名前の変更] コマンドを使用すると、コードと config ファイルの両方で同時にインターフェイス名 "IService1" を変更できます。
<ServiceContract()>
Public Interface IService1

    <OperationContract()>
    Function GetData(ByVal value As Integer) As String

    <OperationContract()>
    Function GetDataUsingDataContract(ByVal composite As CompositeType) As CompositeType

    ' TODO: ここにサービス操作を追加します。
    <OperationContract()>
    Function getReports単純なサンプル() As Byte()
    <OperationContract()>
    Function getReports10の倍数() As Byte()
    <OperationContract()>
    Function getReports見積書() As Byte()
    <OperationContract()>
    Function getReports郵便番号() As Byte()
    <OperationContract()>
    Function getReports広告() As Byte()
    <OperationContract()>
    Function getReports請求書() As Byte()
    <OperationContract()>
    Function getReports商品一覧() As Byte()

End Interface

' サービス操作に複合型を追加するには、以下のサンプルに示すようにデータ コントラクトを使用します。

<DataContract()>
Public Class CompositeType

    <DataMember()>
    Public Property BoolValue() As Boolean

    <DataMember()>
    Public Property StringValue() As String

End Class
