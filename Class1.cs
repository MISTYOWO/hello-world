using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Autodesk.Revit.DB;  //数据库
using Autodesk.Revit.UI;  //
using Autodesk.Revit.Attributes; //命令的属性
using Autodesk.Revit.ApplicationServices;
using System.Windows.Forms;  //弹窗
using Autodesk.Revit.UI.Selection;


namespace helloworld
{
    [Transaction(TransactionMode.Automatic)]    //事务属性 自动api帮你提交，手动自己提交
    [Regeneration(RegenerationOption.Manual)] //automatic 更新模式属性 manual需要自己regenerate;效率高，automatic 立即反馈到模型
    public class mistyhelloworld : IExternalCommand   //外部命令接口 派生
    {
        public Result Execute(ExternalCommandData commandData, ref string messages, ElementSet elements)
        {
            MessageBox.Show("hello world misty");
            return Result.Succeeded;
        }
    }

    //  [Transaction(TransactionMode.Automatic)]    //事务属性
    //  [Regeneration(RegenerationOption.Manual)] //automatic 更新模式属性
    //  public class argumentusage : IExternalCommand   //外部命令接口 派生
    //  {
    //  public Result Execute(ExternalCommandData commandData, ref string messages, ElementSet elements)
    //     {
    //     UIApplication uiapp = commandData.Application;
    //   Autodesk.Revit.ApplicationServices.Application app = uiapp.Application;
    //  Document doc = uiapp.ActiveUIDocument.Document;

    // string strappinfo = app.VersionBuild + ";" + app.VersionName + ";" + app.VersionNumber;
    //  Autodesk.Revit.UI.Selection.Selection sel = uiapp.ActiveUIDocument.Selection;
    //        foreach (Element elem in sel.ELEMENTS )  函数中不包含elements
    //         {
    //           elements.Insert(elem);
    // }
    //  messages = "当前选择集中包含如下对象";
    //为了显示错误信息框，需要返回failed
    //      return Result.Failed;


    // }
    // }





    [Transaction(TransactionMode.Automatic)]    //事务属性 自动api帮你提交，手动自己提交
    [Regeneration(RegenerationOption.Manual)] //automatic 更新模式属性 manual需要自己regenerate;效率高，automatic 立即反馈到模型
    public class yingyong1 : IExternalApplication   //外部命令接口 派生
    {
        public Result OnStartup(UIControlledApplication app)
        {
            MessageBox.Show("revit is starting");
            return Result.Succeeded;

        }
        public Result OnShutdown(UIControlledApplication app)
        {
            MessageBox.Show("revit is existing");
            return Result.Succeeded;

        }
    }


    [Transaction(TransactionMode.Automatic)]    //事务属性 自动api帮你提交，手动自己提交
    [Regeneration(RegenerationOption.Manual)] //automatic 更新模式属性 manual需要自己regenerate;效率高，automatic 立即反馈到模型
    public class getallwindows : IExternalCommand   //外部命令接口 派生
    {
        public Result Execute(ExternalCommandData commandData, ref string messages, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            Autodesk.Revit.ApplicationServices.Application app = uiapp.Application;
            Document doc = uiapp.ActiveUIDocument.Document;

            //获得所有窗户
            FilteredElementCollector collector = new FilteredElementCollector(doc);
            collector.OfClass(typeof(FamilyInstance)).OfCategory(BuiltInCategory.OST_Windows);
            IList<Element> lists = collector.ToElements();
            string strNsg = string.Format("there are {0} windows in current models", lists.Count);
            MessageBox.Show(strNsg);

            //过滤器获得所有的门
            FilteredElementCollector collector2 = new FilteredElementCollector(doc);
            ElementClassFilter classFilter = new ElementClassFilter(typeof(FamilyInstance));
            ElementCategoryFilter catfilter = new ElementCategoryFilter(BuiltInCategory.OST_Doors);
            LogicalAndFilter logicalfilter = new LogicalAndFilter(classFilter, catfilter);
            collector2.WherePasses(logicalfilter);
            IList<Element> list2 = collector2.ToElements();
            strNsg = string.Format("there Area {0} doors in current mode", list2.Count);
            MessageBox.Show(strNsg);
            return Result.Succeeded;
        }
    }


    // public class getallwindowsinlevl1 : IExternalCommand   //外部命令接口 派生
    //{
    //  public Result Execute(ExternalCommandData commandData, ref string messages, ElementSet elements)
    //  {
    //    UIApplication uiapp = commandData.Application;
    //  Autodesk.Revit.ApplicationServices.Application app = uiapp.Application;
    //  Document doc = uiapp.ActiveUIDocument.Document;

    //获得所有窗户
    //FilteredElementCollector collector = new FilteredElementCollector(doc);
    //  collector.OfClass(typeof(FamilyInstance)).OfCategory(BuiltInCategory.OST_Windows);
    //  IList<Element> lists = collector.ToElements();


    //get windows in level 1
    //  var windowInlevel1 = from element in collector
    //                       where element.Level.Name == "Level 1"
    //                       select element;
    //  string strNsg = string.Format("there are {0} windows in level 1", windowInlevel1.Count());
    //  MessageBox.Show(strNsg);

    //return Result.Succeeded;
    //}
    //    }




    [Transaction(TransactionMode.Automatic)]    //事务属性 自动api帮你提交，手动自己提交
    [Regeneration(RegenerationOption.Manual)] //automatic 更新模式属性 manual需要自己regenerate;效率高，automatic 立即反馈到模型
    public class getallparametersvalue : IExternalCommand   //外部命令接口 派生
    {
        public Result Execute(ExternalCommandData commandData, ref string messages, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            Autodesk.Revit.ApplicationServices.Application app = uiapp.Application;
            Document doc = uiapp.ActiveUIDocument.Document;

            Selection sel = uiapp.ActiveUIDocument.Selection;

            // please select an element
            sel.StatusbarTip = "please select an element";
            sel.Pickone();
            Element elemPick = null;

            foreach (Element elem in sel.element)
            {
                       elemPick = elem;
                        break;
        }
           string strparaminfo = null;
            foreach(Parameter param in elemPick.Parameters)
            {
                if (param.AsValueString() != null)
                    strparaminfo += param.Definition.Name + "value is :" + param.AsValueString() + "\n";
                else
                    strparaminfo += param.Definition.Name + "value is :" + param.AsString() + "\n";
            }
            MessageBox.Show(strparaminfo);
            return Result.Succeeded;
        }
    }
}
