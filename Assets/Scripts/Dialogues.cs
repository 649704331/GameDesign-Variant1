using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Xml;
using System.Collections.Generic;//用到了容器类  
 
public class Dialogues : MonoBehaviour {


    //这是场景中的各个物体
    [SerializeField] 
    private GameObject roleA;
    [SerializeField]
    private GameObject roleB;
    [SerializeField]
    private GameObject roleC;
    [SerializeField]
    private GameObject roleNameA;
    [SerializeField]
    private GameObject roleNameB;
    [SerializeField]
    private GameObject roleNameC;
    [SerializeField]
    private GameObject detail;
    private List<string> dialogues_list;//存放dialogues的list
    private int dialogue_index =0 ;//对话索引
    private int dialogue_count = 0;//对话数量
 
    private string role;//当前在说话的角色
    private string role_detail;//当前在说话的内容。

    public GameObject teachCanvas;
 
	void Start () {
        //变量初始化
        /*
        roleA = GameObject.Find("Canvas/roleA");
        roleB = GameObject.Find("Canvas/roleB");
        roleNameA = GameObject.Find("Canvas/Image/roleNameA");
        roleNameB = GameObject.Find("Canvas/Image/roleNameB");
        detail = GameObject.Find("Canvas/Image/detail");
         **/
        teachCanvas.SetActive(false);
        roleA.SetActive(false);
        roleB.SetActive(false);
        roleC.SetActive(false);
        XmlDocument xmlDocument = new XmlDocument();//新建一个XML“编辑器”  
        dialogues_list = new List<string>();//初始化存放dialogues的list
        //载入资源文件
        string data = Resources.Load("dialogues3").ToString();//注意这里没有后缀名xml。你可以看看编辑器中，也是不带后缀的。因此不要有个同名的其它格式文件注意！
        //如果Resources下又有目录那就：Resources.Load("xx\\xx\\dialogues").ToString()
        xmlDocument.LoadXml(data);//载入这个xml  
        XmlNodeList xmlNodeList = xmlDocument.SelectSingleNode("dialogues").ChildNodes;//选择<dialogues>为根结点并得到旗下所有子节点  
        foreach (XmlNode xmlNode in xmlNodeList)//遍历<dialogues>下的所有节点<dialogue>压入List
        {
            XmlElement xmlElement = (XmlElement)xmlNode;//对于任何一个元素，其实就是每一个<dialogue>  
            dialogues_list.Add(xmlElement.ChildNodes.Item(0).InnerText + "," + xmlElement.ChildNodes.Item(1).InnerText);
            //将角色名和对话内容存入这个list，中间存个逗号一会儿容易分割
        }
        dialogue_count = dialogues_list.Count;//获取到底有多少条对话
        Data.isGame = false;
        dialogues_handle(0);
        //载入第一条对话的场景
    }
	
	void Update () {
     
        if (Input.GetMouseButtonDown(0))//如果点击了鼠标左键
        {
            dialogue_index++;//对话跳到一下个
            if (dialogue_index < dialogue_count)//如果对话还没有完
            {
                dialogues_handle(dialogue_index);//那就载入下一条对话
            }
            else { //对话完了
                gameObject.SetActive(false);
                teachCanvas.SetActive(true);
                //进入下一游戏场景之类的
                Data.isGame = true;
            }
        }  
	}
 
    /*处理每一条对话的函数，就是将dialogues_list每一条对话弄到场景*/
    private void dialogues_handle(int dialogue_index) {
        //切割数组
        string[] role_detail_array = dialogues_list[dialogue_index].Split(',');//list中每一个对话格式就是“角色名,对话”
        role = role_detail_array[0];
        role_detail = role_detail_array[1];
 
        switch (role)//根据角色名
        {   //显示当前说话的角色
            case "珏":
                roleA.SetActive(true);
                roleB.SetActive(false);
                roleC.SetActive(false);
                roleNameA.SetActive(true);
                roleNameB.SetActive(false);
                roleNameC.SetActive(false);
                roleNameA.GetComponent<Text>().text = "珏:";
                break;
            case "父亲":
                roleB.SetActive(true);
                roleA.SetActive(false);
                roleC.SetActive(false);
                roleNameB.SetActive(true);
                roleNameA.SetActive(false);
                roleNameC.SetActive(false);
                roleNameB.GetComponent<Text>().text = "父亲:";
                break;
            case "琦":
                roleC.SetActive(true);
                roleA.SetActive(false);
                roleB.SetActive(false);
                roleNameC.SetActive(true);
                roleNameA.SetActive(false);
                roleNameB.SetActive(false);
                roleNameC.GetComponent<Text>().text = "琦:";
                break;
        }
        detail.GetComponent<Text>().text = role_detail;//并加载当前的对话
    }
 
}
