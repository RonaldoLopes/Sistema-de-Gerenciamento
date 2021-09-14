using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SG.Repository.Context;

namespace SG.WebApi.Controllers
{
    [Route("api/reldd")]
    [ApiController]
    public class RelDadosDiarioExcel : ControllerBase
    {
        [HttpGet]
        [Route("{codProjet:int}/{userId:int}/{dataIni:DateTime}/{dataFim:DateTime}")]
        [AllowAnonymous]
        // public async Task<IActionResult<CadernoHoras>> ExportExcelCH()
        public async Task<IActionResult> GetByUserProjet([FromServices] SGContext context, 
            DateTime dataIni, DateTime dataFim, int codProjet, int userId)
        {
            try
            {
                using var workbook = new XLWorkbook();
                var worksheet = workbook.Worksheets.Add("DD");

                var dd = await context.DadosDias
                   .Include(p => p.Projetos)
                   .Where(dd => dd.UserId == userId && dd.ProjetosId == codProjet && dd.Data >= dataIni && dd.Data <= dataFim)
                   .OrderBy(p => p.Data)
                   .AsNoTracking()
                   .ToListAsync();

                #region cabeçalho
                worksheet.Cell("A1").Value = "Data";
                var lineA1Range = worksheet.Range("A1");
                lineA1Range.Merge().Style.Font.SetBold().Font.FontSize = 10;
                lineA1Range.Style.Fill.BackgroundColor = XLColor.FromArgb(169, 169, 169);
                lineA1Range.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                worksheet.Cell("B1").Value = "Saída Hotel";
                var lineB1Range = worksheet.Range("B1");
                lineB1Range.Merge().Style.Font.SetBold().Font.FontSize = 10;
                lineB1Range.Style.Fill.BackgroundColor = XLColor.FromArgb(169, 169, 169);
                lineB1Range.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                worksheet.Cell("C1").Value = "Entrada Fábrica";
                var lineC1Range = worksheet.Range("C1");
                lineC1Range.Merge().Style.Font.SetBold().Font.FontSize = 10;
                lineC1Range.Style.Fill.BackgroundColor = XLColor.FromArgb(169, 169, 169);
                lineC1Range.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                worksheet.Cell("D1").Value = "Saída Almoço";
                var lineD1Range = worksheet.Range("D1");
                lineD1Range.Merge().Style.Font.SetBold().Font.FontSize = 10;
                lineD1Range.Style.Fill.BackgroundColor = XLColor.FromArgb(169, 169, 169);
                lineD1Range.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                worksheet.Cell("E1").Value = "Retorno Almoço";
                var lineE1Range = worksheet.Range("E1");
                lineE1Range.Merge().Style.Font.SetBold().Font.FontSize = 10;
                lineE1Range.Style.Fill.BackgroundColor = XLColor.FromArgb(169, 169, 169);
                lineE1Range.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                worksheet.Cell("F1").Value = "Saída Lanche";
                var lineF1Range = worksheet.Range("F1");
                lineF1Range.Merge().Style.Font.SetBold().Font.FontSize = 10;
                lineF1Range.Style.Fill.BackgroundColor = XLColor.FromArgb(169, 169, 169);
                lineF1Range.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                worksheet.Cell("G1").Value = "Retorno Lanche";
                var lineG1Range = worksheet.Range("G1");
                lineG1Range.Merge().Style.Font.SetBold().Font.FontSize = 10;
                lineG1Range.Style.Fill.BackgroundColor = XLColor.FromArgb(169, 169, 169);
                lineG1Range.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                worksheet.Cell("H1").Value = "Saída Fábrica";
                var lineH1Range = worksheet.Range("H1");
                lineH1Range.Merge().Style.Font.SetBold().Font.FontSize = 10;
                lineH1Range.Style.Fill.BackgroundColor = XLColor.FromArgb(169, 169, 169);
                lineH1Range.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                worksheet.Cell("I1").Value = "Chegada Hotel";
                var lineI1Range = worksheet.Range("I1");
                lineI1Range.Merge().Style.Font.SetBold().Font.FontSize = 10;
                lineI1Range.Style.Fill.BackgroundColor = XLColor.FromArgb(169, 169, 169);
                lineI1Range.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                worksheet.Cell("J1").Value = "Projeto";
                var lineJ1Range = worksheet.Range("J1");
                lineJ1Range.Merge().Style.Font.SetBold().Font.FontSize = 10;
                lineJ1Range.Style.Fill.BackgroundColor = XLColor.FromArgb(169, 169, 169);
                lineJ1Range.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                worksheet.Cell("K1").Value = "Atividades do dia";
                var lineK1Range = worksheet.Range("K1");
                lineK1Range.Merge().Style.Font.SetBold().Font.FontSize = 10;
                lineK1Range.Style.Fill.BackgroundColor = XLColor.FromArgb(169, 169, 169);
                lineK1Range.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                #endregion

                var currentRow = 2;
                foreach (var item in dd)
                {
                    worksheet.Cell(currentRow, 1).Value = item.Data.Date;
                    worksheet.Cell(currentRow, 2).Value = item.SaidaHotel;
                    worksheet.Cell(currentRow, 3).Value = item.EntraFabrica;
                    worksheet.Cell(currentRow, 4).Value = item.SaidaAlmo;
                    worksheet.Cell(currentRow, 5).Value = item.RetorAlmo;
                    worksheet.Cell(currentRow, 6).Value = item.SaidaLanche;

                    worksheet.Cell(currentRow, 7).Value = item.RetorLanche;
                    worksheet.Cell(currentRow, 8).Value = item.SaidaFabrica;
                    worksheet.Cell(currentRow, 9).Value = item.ChegaHotel;
                    worksheet.Cell(currentRow, 10).Value = item.Projetos.CodProjeto;
                    worksheet.Cell(currentRow, 11).Value = item.AtvDia;
                    currentRow++;
                }

                await using var memory = new MemoryStream();
                workbook.SaveAs(memory);

                return File(memory.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "dadosdiarios.xlsx");
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
