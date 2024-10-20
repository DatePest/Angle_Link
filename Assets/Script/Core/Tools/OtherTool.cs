using dnlib.DotNet.MD;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class OtherTool
{
    static DataTable dataTable;
    static DataTable dataTableGet()
    {
        if (dataTable == null)
        {
            dataTable = new DataTable(); 
        }
        return dataTable;
    }
    public static float StringCalculateFormulaCompute(string expression,string filter = "")
    {
        var result = dataTableGet().Compute(expression, filter);

        try
        {
            return Convert.ToSingle(result) ;
        }
        catch (InvalidCastException ex)  // 捕捉轉型相關異常
        {
            throw new Exception($"Invalid cast error during Compute. Expression: {expression}", ex);
        }
        catch (FormatException ex)  // 捕捉格式錯誤
        {
            throw new Exception($"Format error during Compute. Expression: {expression}", ex);
        }
        catch (Exception ex)  // 捕捉所有其他未預期的錯誤
        {
            throw new Exception($"Unknown error during Compute. Expression: {expression}", ex);
        }
    }




    /// <summary>
    /// 将一个标准的GUID转换成短的字符串如：3d4ebc5f5f2c4ede,生成1亿次都不会出现重复。
    /// </summary>
    /// <returns></returns>
    public static string GenerateStringID()
    {
        long i = 1;
        foreach (byte b in Guid.NewGuid().ToByteArray())
        {
            i *= ((int)b + 1);
        }
        return string.Format("{0:x}", i - DateTime.Now.Ticks);
    }


}