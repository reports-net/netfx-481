using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WCFServiceWebRole1
{
    // メモ: [リファクター] メニューの [名前の変更] コマンドを使用すると、コードと config ファイルの両方で同時にインターフェイス名 "IService1" を変更できます。
    [ServiceContract]
    public interface IService1
    {

        [OperationContract]
        string GetData(int value);

        [OperationContract]
        CompositeType GetDataUsingDataContract(CompositeType composite);

        // TODO: ここにサービス操作を追加します。
        [OperationContract]
        byte[] getReports単純なサンプル();
        [OperationContract]
        byte[] getReports10の倍数();
        [OperationContract]
        byte[] getReports見積書();
        [OperationContract]
        byte[] getReports郵便番号();
        [OperationContract]
        byte[] getReports広告();
        [OperationContract]
        byte[] getReports請求書();
        [OperationContract]
        byte[] getReports商品一覧();


    }


    // サービス操作に複合型を追加するには、以下のサンプルに示すようにデータ コントラクトを使用します。
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }
}
