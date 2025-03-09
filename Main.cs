using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Microsoft.Win32;
using Prism.Dialogs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Module_5_8_1
{
    [Transaction(TransactionMode.Manual)]
    public class Main : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {


            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;

            using (var ts = new Transaction(doc, "export ifc"))
            {
                ts.Start();

                string directoryPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                string fileName = "export.ifc";


                IFCExportOptions ifcOptions = new IFCExportOptions();

                try
                {
                    // Экспорт модели
                    doc.Export(directoryPath, fileName, ifcOptions);
                    TaskDialog.Show("Успех", $"Файл IFC сохранен: {directoryPath}");
                    return Result.Succeeded;
                }
                catch (Exception ex)
                {
                    TaskDialog.Show("Ошибка", $"Ошибка экспорта: {ex.Message}");
                    return Result.Failed;
                }

                ts.Commit();
            }


            

        }
    }
}
