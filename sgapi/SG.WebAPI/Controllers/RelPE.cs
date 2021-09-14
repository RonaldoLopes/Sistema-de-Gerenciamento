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
    [Route("api/relpe")]
    [ApiController]
    [Authorize(Roles = "Admin, Gestor, RUser")]
    public class RelPE : ControllerBase
    {
        [HttpGet]
        [Route("{dataIni:DateTime}/{dataFim:DateTime}/{userId:int}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetByUserProjet([FromServices] SGContext context, DateTime dataIni, DateTime dataFim, int userId)
        {
            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("PE");
            var nomeUser = "";
            var setorUser = "";
            var funcaoUser = "";
            var mes = "";
            var hExtraMDS = TimeSpan.Parse("09:00:00");
            var hExtraFDS = TimeSpan.Parse("08:00:00");

            var pe = await context.PontoExternos
                .Include(p => p.Projetos)
                .Include(u => u.User)
                .Where(pe => pe.Data >= dataIni && pe.Data <= dataFim && pe.UserId == userId)
                .OrderBy(pe => pe.Data)
                .AsNoTracking()
                .ToListAsync();

            #region cabeçalho
            worksheet.Cell("A1").Value = "Registro de ponto externo";
            var lineA1Range = worksheet.Range("A1:I1");
            lineA1Range.Merge().Style.Font.SetBold().Font.FontSize = 10;
            lineA1Range.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            worksheet.Cell("J1").Value = "Hora Extra Calculo";
            var lineJ1Range = worksheet.Range("J1:K1");
            lineJ1Range.Merge().Style.Font.SetBold().Font.FontSize = 10;
            lineJ1Range.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            worksheet.Cell("J2").Value = "Segunda a sexta 50%";
            var lineJ2Range = worksheet.Range("J2:K2");
            lineJ2Range.Merge().Style.Font.SetBold().Font.FontSize = 10;
            lineJ2Range.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            worksheet.Cell("J3").Value = "FDS e Feriado 100%";
            var lineJ3Range = worksheet.Range("J3:K3");
            lineJ3Range.Merge().Style.Font.SetBold().Font.FontSize = 10;
            lineJ3Range.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            worksheet.Cell("J4").Value = "Carga horária 09:00 Seg/Quinta, 08:00 Sexta";
            var lineJ4Range = worksheet.Range("J4:K4");
            lineJ4Range.Merge().Style.Font.SetBold().Font.FontSize = 10;
            lineJ4Range.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            worksheet.Cell("A2").Value = "Nome";
            var lineA2Range = worksheet.Range("A2");
            lineA2Range.Merge().Style.Font.SetBold().Font.FontSize = 10;
            lineA2Range.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            worksheet.Cell("A3").Value = "Setor";
            var lineA3Range = worksheet.Range("A3");
            lineA3Range.Merge().Style.Font.SetBold().Font.FontSize = 10;
            lineA3Range.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            worksheet.Cell("A4").Value = "Função";
            var lineA4Range = worksheet.Range("A4");
            lineA4Range.Merge().Style.Font.SetBold().Font.FontSize = 10;
            lineA4Range.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            worksheet.Cell("A5").Value = "Mês";
            var lineA5Range = worksheet.Range("A5");
            lineA5Range.Merge().Style.Font.SetBold().Font.FontSize = 10;
            lineA5Range.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            mes = dataIni.ToString("MMMM").ToUpper();
            worksheet.Cell("B5").Value = mes;
            var lineB5Range = worksheet.Range("B5:I5");
            lineB5Range.Merge().Style.Font.SetBold().Font.FontSize = 10;
            lineB5Range.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            worksheet.Cell("A6").Value = "Data";
            var lineA6Range = worksheet.Range("A6");
            lineA6Range.Merge().Style.Font.SetBold().Font.FontSize = 10;
            lineA6Range.Style.Fill.BackgroundColor = XLColor.FromArgb(169, 169, 169);
            lineA6Range.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;   

            worksheet.Cell("B6").Value = "Dia";
            var lineB6Range = worksheet.Range("B6");
            lineB6Range.Merge().Style.Font.SetBold().Font.FontSize = 10;
            lineB6Range.Style.Fill.BackgroundColor = XLColor.FromArgb(169, 169, 169);
            lineB6Range.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            worksheet.Cell("C6").Value = "Entrada";
            var lineC6Range = worksheet.Range("C6");
            lineC6Range.Merge().Style.Font.SetBold().Font.FontSize = 10;
            lineC6Range.Style.Fill.BackgroundColor = XLColor.FromArgb(169, 169, 169);
            lineC6Range.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            worksheet.Cell("D6").Value = "Saída Almoço";
            var lineD6Range = worksheet.Range("D6");
            lineD6Range.Merge().Style.Font.SetBold().Font.FontSize = 10;
            lineD6Range.Style.Fill.BackgroundColor = XLColor.FromArgb(169, 169, 169);
            lineD6Range.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            worksheet.Cell("E6").Value = "Retorno Almoço";
            var lineE6Range = worksheet.Range("E6");
            lineE6Range.Merge().Style.Font.SetBold().Font.FontSize = 10;
            lineE6Range.Style.Fill.BackgroundColor = XLColor.FromArgb(169, 169, 169);
            lineE6Range.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            worksheet.Cell("F6").Value = "Saída";
            var lineF6Range = worksheet.Range("F6");
            lineF6Range.Merge().Style.Font.SetBold().Font.FontSize = 10;
            lineF6Range.Style.Fill.BackgroundColor = XLColor.FromArgb(169, 169, 169);
            lineF6Range.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            worksheet.Cell("G6").Value = "Total de horas trabalhas";
            var lineG6Range = worksheet.Range("G6");
            lineG6Range.Merge().Style.Font.SetBold().Font.FontSize = 10;
            lineG6Range.Style.Fill.BackgroundColor = XLColor.FromArgb(169, 169, 169);
            lineG6Range.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            worksheet.Cell("H6").Value = "Justificatica";
            var lineH6Range = worksheet.Range("H6");
            lineH6Range.Merge().Style.Font.SetBold().Font.FontSize = 10;
            lineH6Range.Style.Fill.BackgroundColor = XLColor.FromArgb(169, 169, 169);
            lineH6Range.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            worksheet.Cell("I6").Value = "Extra";
            var lineI6Range = worksheet.Range("I6");
            lineI6Range.Merge().Style.Font.SetBold().Font.FontSize = 10;
            lineI6Range.Style.Fill.BackgroundColor = XLColor.FromArgb(169, 169, 169);
            lineI6Range.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            #endregion

            var currentRow = 7;
            TimeSpan horasTrab;

            foreach (var item in pe)
            {
                //mes = dataIni.ToString("MMMM").ToUpper();
                nomeUser = item.User.FullName;
                setorUser = item.User.Setor;
                funcaoUser = item.User.Funcao;

                worksheet.Cell(currentRow, 1).Value = item.Data.Date;
                worksheet.Cell(currentRow, 2).Value = item.Data.ToString("dddd");
                worksheet.Cell(currentRow, 3).Value = item.EntraFabrica;
                worksheet.Cell(currentRow, 4).Value = item.SaidaAlmo;
                worksheet.Cell(currentRow, 5).Value = item.RetorAlmo;


                worksheet.Cell(currentRow, 6).Value = item.SaidaFabrica;
                horasTrab = (TimeSpan)(item.SaidaFabrica - item.EntraFabrica);
                worksheet.Cell(currentRow, 7).Value = horasTrab;
                worksheet.Cell(currentRow, 8).Value = item.AtvDia;

                switch (item.Data.ToString("dddd"))
                {
                    case "segunda-feira":                        
                        if (horasTrab > hExtraMDS) {
                            worksheet.Cell(currentRow, 9).Value = ((horasTrab - hExtraMDS)/2) + (horasTrab - hExtraMDS);
                        } 
                        break;
                    case "terça-feira":
                        if (horasTrab > hExtraMDS)
                        {
                            worksheet.Cell(currentRow, 9).Value = ((horasTrab - hExtraMDS) / 2) + (horasTrab - hExtraMDS);
                        }
                        break;
                    case "quarta-feira":
                        if (horasTrab > hExtraMDS)
                        {
                            worksheet.Cell(currentRow, 9).Value = ((horasTrab - hExtraMDS) / 2) + (horasTrab - hExtraMDS);
                        }
                        break;
                    case "quinta-feira":
                        if (horasTrab > hExtraMDS)
                        {
                            worksheet.Cell(currentRow, 9).Value = ((horasTrab - hExtraMDS) / 2) + (horasTrab - hExtraMDS);
                        }
                        break;
                    case "sexta-feira":
                        if (horasTrab > hExtraMDS)
                        {
                            worksheet.Cell(currentRow, 9).Value = ((horasTrab - hExtraMDS) / 2) + (horasTrab - hExtraMDS);
                        }
                        break;
                    case "sábado":
                        if (horasTrab > hExtraMDS)
                        {
                            worksheet.Cell(currentRow, 9).Value = ((horasTrab - hExtraMDS) * 2);
                        }
                        break;
                    case "domingo":
                        if (horasTrab > hExtraMDS)
                        {
                            worksheet.Cell(currentRow, 9).Value = ((horasTrab - hExtraMDS) * 2);
                        }
                        break;
                    default:
                        break;
                }

                
                currentRow++;
            }

            worksheet.Cell("B2").Value = nomeUser;
            var lineB2Range = worksheet.Range("B2:I2");
            lineB2Range.Merge().Style.Font.SetBold().Font.FontSize = 10;
            lineB2Range.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            worksheet.Cell("B3").Value = setorUser;
            var lineB3Range = worksheet.Range("B3:I3");
            lineB3Range.Merge().Style.Font.SetBold().Font.FontSize = 10;
            lineB3Range.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            worksheet.Cell("B4").Value = funcaoUser;
            var lineB4Range = worksheet.Range("B4:I4");
            lineB4Range.Merge().Style.Font.SetBold().Font.FontSize = 10;
            lineB4Range.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            await using var memory = new MemoryStream();
            workbook.SaveAs(memory);

            return File(memory.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "pontoexterno.xlsx");
        }
    }
}
