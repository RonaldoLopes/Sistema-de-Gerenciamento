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
    [Route("api/relch")]
    [ApiController]
    public class CadernoHorasExcel : ControllerBase
    {
        [HttpGet]
        [Route("{codProjet:int}/{userId:int}")]
        [AllowAnonymous]
       // public async Task<IActionResult<CadernoHoras>> ExportExcelCH()
        public async Task<IActionResult> GetByUserProjet([FromServices] SGContext context, int codProjet, int userId)
        {
            var userName = "";
            var recursos = 0;
            var localCid = "";
            var localEst = "";
            var mobilizacoes = 0;
            var hprvimplement = 0;
            var deslocSum = new TimeSpan(0, 0, 0);
            var totHorasTrab = new TimeSpan(0, 0, 0);
            var projet = "";
            var dataInicio = new DateTime();
            var areaProje = "";

            try
            {
                using var workbook = new XLWorkbook();
                var worksheet = workbook.Worksheets.Add("CHI");

                var  result = await (from ch in context.CadernoHoras
                             join p in context.Projetos on ch.ProjetosId equals p.Id
                             join usr in context.Users on ch.UserId equals usr.Id
                             join cli in context.Clientes on p.ClienteId equals cli.Id
                             where(p.Id == codProjet  && ch.UserId == userId)
                             select new
                             {
                                 id = ch.Id,
                                 data = p.DataInicio,
                                 horasd = ch.HorasDia,
                                 deslocamento = ch.Deslocamento,
                                 horastrab = ch.HorasTrab,
                                 codproj = p.CodProjeto,
                                 localprocid = cli.Cidade,
                                 localprodest = cli.Estado,
                                 area = cli.Area,
                                 recursutil = p.RecursosUtil,
                                 mobiliutil = p.MobilizaUtili,
                                 usuario = usr.UserName,
                                 datach = ch.Data,
                                 atvdia = ch.AtvDia,
                                 hprevimplement = p.HorasPrevImplement
                             }).ToListAsync();

                var result1 = result.GroupBy(r => r.id);


                 var titleRange = worksheet.Range("A1:D1");
                 worksheet.Cell(1, 1).Value = "FORMULÁRIO RISC AUTOMAÇÃO";
                 titleRange.Merge().Style.Font.SetBold().Font.FontSize = 10;
                 titleRange.Style.Border.TopBorder = XLBorderStyleValues.Thin;
                 titleRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                 titleRange.Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                 titleRange.Style.Border.RightBorder = XLBorderStyleValues.Thin;
                 titleRange.Style.Border.TopBorder = XLBorderStyleValues.Thin;
                 titleRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                 worksheet.Cell("A2").Value = "Título:";
                 var line2Range = worksheet.Range("A2:D2");
                 line2Range.Merge().Style.Font.SetBold().Font.FontSize = 10;
                 line2Range.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                 line2Range.Style.Border.TopBorder = XLBorderStyleValues.Thin;
                 line2Range.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                 line2Range.Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                 line2Range.Style.Border.RightBorder = XLBorderStyleValues.Thin;
                 line2Range.Style.Border.TopBorder = XLBorderStyleValues.Thin;

                 worksheet.Cell("A3").Value = "CHI - CONTROLE DE HORAS DE IMPLEMENTAÇÃO";
                 var line3Range = worksheet.Range("A3:D3");
                 line3Range.Merge().Style.Font.FontSize = 10;
                 line3Range.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                 line3Range.Style.Border.TopBorder = XLBorderStyleValues.Thin;
                 line3Range.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                 line3Range.Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                 line3Range.Style.Border.RightBorder = XLBorderStyleValues.Thin;
                 line3Range.Style.Border.TopBorder = XLBorderStyleValues.Thin;

                worksheet.Cell("A16").Value = "Data ";
                var lineA16Range = worksheet.Range("A16");
                lineA16Range.Merge().Style.Font.SetFontColor(XLColor.White).Font.SetBold(); ;
                lineA16Range.Style.Fill.BackgroundColor = XLColor.FromArgb(0, 153, 153);
                lineA16Range.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                lineA16Range.Style.Border.TopBorder = XLBorderStyleValues.Thin;
                lineA16Range.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                lineA16Range.Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                lineA16Range.Style.Border.RightBorder = XLBorderStyleValues.Thin;
                lineA16Range.Style.Border.TopBorder = XLBorderStyleValues.Thin;

                worksheet.Cell("B16").Value = "Projeto ";
                var lineB16Range = worksheet.Range("B16");
                lineB16Range.Merge().Style.Font.SetFontColor(XLColor.White).Font.SetBold(); ;
                lineB16Range.Style.Fill.BackgroundColor = XLColor.FromArgb(0, 153, 153);
                lineB16Range.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                worksheet.Cell("C16").Value = "Atividades ";
                var lineC16Range = worksheet.Range("C16");
                lineC16Range.Merge().Style.Font.SetFontColor(XLColor.White).Font.SetBold(); ;
                lineC16Range.Style.Fill.BackgroundColor = XLColor.FromArgb(0, 153, 153);
                lineC16Range.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                lineC16Range.Style.Border.TopBorder = XLBorderStyleValues.Thin;
                lineC16Range.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                lineC16Range.Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                lineC16Range.Style.Border.RightBorder = XLBorderStyleValues.Thin;
                lineC16Range.Style.Border.TopBorder = XLBorderStyleValues.Thin;

                worksheet.Cell("D16").Value = "Horas Dia ";
                var lineD16Range = worksheet.Range("D16");
                lineD16Range.Merge().Style.Font.SetFontColor(XLColor.White).Font.SetBold(); ;
                lineD16Range.Style.Fill.BackgroundColor = XLColor.FromArgb(0, 153, 153);
                lineD16Range.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                lineD16Range.Style.Border.TopBorder = XLBorderStyleValues.Thin;
                lineD16Range.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                lineD16Range.Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                lineD16Range.Style.Border.RightBorder = XLBorderStyleValues.Thin;
                lineD16Range.Style.Border.TopBorder = XLBorderStyleValues.Thin;

                worksheet.Cell("E16").Value = "Deslocamento ";
                var lineE16Range = worksheet.Range("E16");
                lineE16Range.Merge().Style.Font.SetFontColor(XLColor.White).Font.SetBold(); ;
                lineE16Range.Style.Fill.BackgroundColor = XLColor.FromArgb(0, 153, 153);
                lineE16Range.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                lineE16Range.Style.Border.TopBorder = XLBorderStyleValues.Thin;
                lineE16Range.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                lineE16Range.Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                lineE16Range.Style.Border.RightBorder = XLBorderStyleValues.Thin;
                lineE16Range.Style.Border.TopBorder = XLBorderStyleValues.Thin;

                worksheet.Cell("F16").Value = "H. Trabalhadas ";
                var lineF16Range = worksheet.Range("F16");
                lineF16Range.Merge().Style.Font.SetFontColor(XLColor.White).Font.SetBold(); ;
                lineF16Range.Style.Fill.BackgroundColor = XLColor.FromArgb(0, 153, 153);
                lineF16Range.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                lineF16Range.Style.Border.TopBorder = XLBorderStyleValues.Thin;
                lineF16Range.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                lineF16Range.Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                lineF16Range.Style.Border.RightBorder = XLBorderStyleValues.Thin;
                lineF16Range.Style.Border.TopBorder = XLBorderStyleValues.Thin;

                var currentRow = 17;
                foreach (var item in result)
                 {
                    userName = item.usuario;
                    localCid = item.localprocid;
                    localEst = item.localprodest;
                    dataInicio = item.data;
                    recursos += item.recursutil;
                    projet = item.codproj;
                    areaProje = item.area;
                    mobilizacoes += item.mobiliutil;
                    hprvimplement = item.hprevimplement;
                    deslocSum += (TimeSpan)item.deslocamento;
                    totHorasTrab += (TimeSpan)item.horastrab;
                    worksheet.Cell(currentRow, 1).Value = item.datach.Date;
                    worksheet.Cell(currentRow, 2).Value = item.codproj;
                    worksheet.Cell(currentRow, 3).Value = item.atvdia;
                    worksheet.Cell(currentRow, 4).Value = item.horasd;
                    worksheet.Cell(currentRow, 5).Value = item.deslocamento;
                    worksheet.Cell(currentRow, 6).Value = item.horastrab;

                    currentRow++;
                }
                //return Ok(result1);
                #region dados projeto
                worksheet.Cell("A5").Value = "Projeto:";
                var line5Range = worksheet.Range("A5");
                line5Range.Style.Fill.BackgroundColor = XLColor.FromArgb(0, 153, 153);
                line5Range.Style.Font.SetFontColor(XLColor.White).Font.SetBold();
                line5Range.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                line5Range.Style.Border.TopBorder = XLBorderStyleValues.Thin;
                line5Range.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                line5Range.Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                line5Range.Style.Border.RightBorder = XLBorderStyleValues.Thin;
                line5Range.Style.Border.TopBorder = XLBorderStyleValues.Thin;

                worksheet.Cell("B5").Value = projet;
                var lineA6Range = worksheet.Range("B5:F5");
                lineA6Range.Merge().Style.Font.SetBold().Font.FontSize = 10;;
                lineA6Range.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                lineA6Range.Style.Border.TopBorder = XLBorderStyleValues.Thin;
                lineA6Range.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                lineA6Range.Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                lineA6Range.Style.Border.RightBorder = XLBorderStyleValues.Thin;
                lineA6Range.Style.Border.TopBorder = XLBorderStyleValues.Thin;
                #endregion

                #region local
                worksheet.Cell("A6").Value = "Local:";
                var line6Range = worksheet.Range("A6");
                line6Range.Style.Fill.BackgroundColor = XLColor.FromArgb(0, 153, 153);
                line6Range.Style.Font.SetFontColor(XLColor.White).Font.SetBold();
                line6Range.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                line6Range.Style.Border.TopBorder = XLBorderStyleValues.Thin;
                line6Range.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                line6Range.Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                line6Range.Style.Border.RightBorder = XLBorderStyleValues.Thin;
                line6Range.Style.Border.TopBorder = XLBorderStyleValues.Thin;

                worksheet.Cell("B6").Value = localCid + " - " + localEst;
                var lineB6Range = worksheet.Range("B6:F6");
                lineB6Range.Merge().Style.Font.SetBold().Font.FontSize = 10; 
                lineB6Range.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                lineB6Range.Style.Border.TopBorder = XLBorderStyleValues.Thin;
                lineB6Range.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                lineB6Range.Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                lineB6Range.Style.Border.RightBorder = XLBorderStyleValues.Thin;
                lineB6Range.Style.Border.TopBorder = XLBorderStyleValues.Thin;
                #endregion

                #region area
                worksheet.Cell("A7").Value = "Área:";
                var line7Range = worksheet.Range("A7");
                line7Range.Style.Fill.BackgroundColor = XLColor.FromArgb(0, 153, 153);
                line7Range.Style.Font.SetFontColor(XLColor.White).Font.SetBold();
                line7Range.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                line7Range.Style.Border.TopBorder = XLBorderStyleValues.Thin;
                line7Range.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                line7Range.Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                line7Range.Style.Border.RightBorder = XLBorderStyleValues.Thin;
                line7Range.Style.Border.TopBorder = XLBorderStyleValues.Thin;

                worksheet.Cell("B7").Value = areaProje;
                var lineB7Range = worksheet.Range("B7:F7");
                lineB7Range.Merge().Style.Font.SetBold().Font.FontSize = 10;
                lineB7Range.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                lineB7Range.Style.Border.TopBorder = XLBorderStyleValues.Thin;
                lineB7Range.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                lineB7Range.Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                lineB7Range.Style.Border.RightBorder = XLBorderStyleValues.Thin;
                lineB7Range.Style.Border.TopBorder = XLBorderStyleValues.Thin;
                #endregion

                #region data_inicio
                worksheet.Cell("A8").Value = "Inicio:";
                var line8Range = worksheet.Range("A8");
                line8Range.Style.Fill.BackgroundColor = XLColor.FromArgb(0, 153, 153);
                line8Range.Style.Font.SetFontColor(XLColor.White).Font.SetBold();
                line8Range.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                line8Range.Style.Border.TopBorder = XLBorderStyleValues.Thin;
                line8Range.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                line8Range.Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                line8Range.Style.Border.RightBorder = XLBorderStyleValues.Thin;
                line8Range.Style.Border.TopBorder = XLBorderStyleValues.Thin;

                worksheet.Cell("B8").Value = dataInicio.Date;
                var lineB8Range = worksheet.Range("B8:F8");
                lineB8Range.Merge().Style.Font.SetBold().Font.FontSize = 10; 
                lineB8Range.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                lineB8Range.Style.Border.TopBorder = XLBorderStyleValues.Thin;
                lineB8Range.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                lineB8Range.Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                lineB8Range.Style.Border.RightBorder = XLBorderStyleValues.Thin;
                lineB8Range.Style.Border.TopBorder = XLBorderStyleValues.Thin;
                #endregion

                #region recursos
                worksheet.Cell("A10").Value = "Recursos:    " + recursos.ToString();
                var line10Range = worksheet.Range("A10:B10");
                line10Range.Merge().Style.Font.SetBold().Font.FontSize = 10;
                line10Range.Style.Fill.BackgroundColor = XLColor.FromArgb(0, 153, 153);
                line10Range.Style.Font.SetFontColor(XLColor.White).Font.SetBold();
                line10Range.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                line10Range.Style.Border.TopBorder = XLBorderStyleValues.Thin;
                line10Range.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                line10Range.Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                line10Range.Style.Border.RightBorder = XLBorderStyleValues.Thin;
                line10Range.Style.Border.TopBorder = XLBorderStyleValues.Thin;
                #endregion

                #region mobilizacoes
                worksheet.Cell("C10").Value = "Mobilizações:     " + mobilizacoes.ToString();
                var lineC10Range = worksheet.Range("C10:D10");
                lineC10Range.Merge().Style.Font.SetBold().Font.FontSize = 10;
                lineC10Range.Style.Fill.BackgroundColor = XLColor.FromArgb(0, 153, 153);
                lineC10Range.Style.Font.SetFontColor(XLColor.White).Font.SetBold();
                lineC10Range.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                lineC10Range.Style.Border.TopBorder = XLBorderStyleValues.Thin;
                lineC10Range.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                lineC10Range.Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                lineC10Range.Style.Border.RightBorder = XLBorderStyleValues.Thin;
                lineC10Range.Style.Border.TopBorder = XLBorderStyleValues.Thin;
                #endregion

                #region horas_previstas_implementacao
                worksheet.Cell("E10").Value = "Horas Previstas Implementação:";
                var lineE10Range = worksheet.Range("E10");
                lineE10Range.Style.Fill.BackgroundColor = XLColor.FromArgb(0, 153, 153);
                lineE10Range.Style.Font.SetFontColor(XLColor.White).Font.SetBold();
                lineE10Range.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                lineE10Range.Style.Border.TopBorder = XLBorderStyleValues.Thin;
                lineE10Range.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                lineE10Range.Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                lineE10Range.Style.Border.RightBorder = XLBorderStyleValues.Thin;
                lineE10Range.Style.Border.TopBorder = XLBorderStyleValues.Thin;

                TimeSpan hpv = new TimeSpan(hprvimplement, 0, 0);
                worksheet.Cell("F10").Value = hpv;
                var lineF10Range = worksheet.Range("F10");
                lineF10Range.Style.Fill.BackgroundColor = XLColor.FromArgb(0, 153, 153);
                lineF10Range.Style.Font.SetFontColor(XLColor.White).Font.SetBold();
                lineF10Range.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                lineF10Range.Style.Border.TopBorder = XLBorderStyleValues.Thin;
                lineF10Range.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                lineF10Range.Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                lineF10Range.Style.Border.RightBorder = XLBorderStyleValues.Thin;
                lineF10Range.Style.Border.TopBorder = XLBorderStyleValues.Thin;
                #endregion

                #region totalizacao geral only title
                worksheet.Cell("A12").Value = "TOTALIZAÇÃO GERAL";
                var lineF12Range = worksheet.Range("A12:F12");
                lineF12Range.Merge().Style.Font.SetFontColor(XLColor.White).Font.SetBold(); ;
                lineF12Range.Style.Fill.BackgroundColor = XLColor.FromArgb(0, 153, 153);
                lineF12Range.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                lineF12Range.Merge().Style.Font.FontSize = 10;
                lineF12Range.Style.Border.TopBorder = XLBorderStyleValues.Thin;
                lineF12Range.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                lineF12Range.Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                lineF12Range.Style.Border.RightBorder = XLBorderStyleValues.Thin;
                lineF12Range.Style.Border.TopBorder = XLBorderStyleValues.Thin;
                #endregion

                #region deslocamento
                worksheet.Cell("A13").Value = "Deslocamento";
                var lineA13Range = worksheet.Range("A13");
                lineA13Range.Style.Fill.BackgroundColor = XLColor.FromArgb(0, 153, 153);
                lineA13Range.Style.Font.SetFontColor(XLColor.White).Font.SetBold();
                lineA13Range.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                lineA13Range.Style.Border.TopBorder = XLBorderStyleValues.Thin;
                lineA13Range.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                lineA13Range.Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                lineA13Range.Style.Border.RightBorder = XLBorderStyleValues.Thin;
                lineA13Range.Style.Border.TopBorder = XLBorderStyleValues.Thin;

                worksheet.Cell("A14").Value = deslocSum;
                var lineA14Range = worksheet.Range("A14");
                lineA14Range.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                lineA14Range.Style.Border.TopBorder = XLBorderStyleValues.Thin;
                lineA14Range.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                lineA14Range.Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                lineA14Range.Style.Border.RightBorder = XLBorderStyleValues.Thin;
                lineA14Range.Style.Border.TopBorder = XLBorderStyleValues.Thin;
                #endregion

                #region total_horas_trabalhadas
                worksheet.Cell("B13").Value = "Total Horas Trab.";
                var lineB13Range = worksheet.Range("B13");
                lineB13Range.Style.Fill.BackgroundColor = XLColor.FromArgb(0, 153, 153);
                lineB13Range.Style.Font.SetFontColor(XLColor.White).Font.SetBold();
                lineB13Range.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                lineB13Range.Style.Border.TopBorder = XLBorderStyleValues.Thin;
                lineB13Range.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                lineB13Range.Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                lineB13Range.Style.Border.RightBorder = XLBorderStyleValues.Thin;
                lineB13Range.Style.Border.TopBorder = XLBorderStyleValues.Thin;

                worksheet.Cell("B14").Value = totHorasTrab;
                var lineB14Range = worksheet.Range("B14");
                lineB14Range.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                lineB14Range.Style.Border.TopBorder = XLBorderStyleValues.Thin;
                lineB14Range.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                lineB14Range.Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                lineB14Range.Style.Border.RightBorder = XLBorderStyleValues.Thin;
                lineB14Range.Style.Border.TopBorder = XLBorderStyleValues.Thin;
                #endregion

                #region restante_setor
                worksheet.Cell("C13").Value = "Restante por setor";
                var lineC13Range = worksheet.Range("C13:D13");
                lineC13Range.Merge().Style.Font.SetBold().Font.FontSize = 10;
                lineC13Range.Style.Fill.BackgroundColor = XLColor.FromArgb(0, 153, 153);
                lineC13Range.Style.Font.SetFontColor(XLColor.White).Font.SetBold();
                lineC13Range.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                lineC13Range.Style.Border.TopBorder = XLBorderStyleValues.Thin;
                lineC13Range.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                lineC13Range.Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                lineC13Range.Style.Border.RightBorder = XLBorderStyleValues.Thin;
                lineC13Range.Style.Border.TopBorder = XLBorderStyleValues.Thin;

               
                var resultado = hpv - totHorasTrab;
               // var final = string.Format("{0}:{1}:{2}", Math.Round(resultado.TotalHours).ToString().PadLeft(2, '0'), resultado.Minutes.ToString().PadLeft(2, '0'), resultado.Seconds.ToString().PadLeft(2, '0'));
                worksheet.Cell("C14").Value = resultado;
                var lineC14Range = worksheet.Range("C14:D14");
                lineC14Range.Merge().Style.Font.FontSize = 10;
                lineC14Range.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                lineC14Range.Style.Border.TopBorder = XLBorderStyleValues.Thin;
                lineC14Range.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                lineC14Range.Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                lineC14Range.Style.Border.RightBorder = XLBorderStyleValues.Thin;
                lineC14Range.Style.Border.TopBorder = XLBorderStyleValues.Thin;
                #endregion

                #region utilizado %
                worksheet.Cell("E13").Value = "Utilizado %";
                var lineE13Range = worksheet.Range("E13");
                lineE13Range.Style.Fill.BackgroundColor = XLColor.FromArgb(0, 153, 153);
                lineE13Range.Style.Font.SetFontColor(XLColor.White).Font.SetBold();
                lineE13Range.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                lineE13Range.Style.Border.TopBorder = XLBorderStyleValues.Thin;
                lineE13Range.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                lineE13Range.Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                lineE13Range.Style.Border.RightBorder = XLBorderStyleValues.Thin;
                lineE13Range.Style.Border.TopBorder = XLBorderStyleValues.Thin;

                var percUtilizado = (totHorasTrab / hpv)*100;
                worksheet.Cell("E14").Value = percUtilizado;
                var lineE14Range = worksheet.Range("E14");
                lineE14Range.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                lineE14Range.Style.Border.TopBorder = XLBorderStyleValues.Thin;
                lineE14Range.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                lineE14Range.Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                lineE14Range.Style.Border.RightBorder = XLBorderStyleValues.Thin;
                lineE14Range.Style.Border.TopBorder = XLBorderStyleValues.Thin;
                #endregion

                #region restante %
                worksheet.Cell("F13").Value = "Restante %";
                var lineF13Range = worksheet.Range("F13");
                lineF13Range.Style.Fill.BackgroundColor = XLColor.FromArgb(0, 153, 153);
                lineF13Range.Style.Font.SetFontColor(XLColor.White).Font.SetBold();
                lineF13Range.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                lineF13Range.Style.Border.TopBorder = XLBorderStyleValues.Thin;
                lineF13Range.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                lineF13Range.Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                lineF13Range.Style.Border.RightBorder = XLBorderStyleValues.Thin;
                lineF13Range.Style.Border.TopBorder = XLBorderStyleValues.Thin;

                //TimeSpan timeC = TimeSpan.Parse(resultado);
                var percRestante = (resultado / hpv) * 100;
                worksheet.Cell("F14").Value = percRestante;
                var lineF14Range = worksheet.Range("F14");
                lineF14Range.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                lineF14Range.Style.Border.TopBorder = XLBorderStyleValues.Thin;
                lineF14Range.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                lineF14Range.Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                lineF14Range.Style.Border.RightBorder = XLBorderStyleValues.Thin;
                lineF14Range.Style.Border.TopBorder = XLBorderStyleValues.Thin;

                
                #endregion

                #region colaborador
                worksheet.Cell("A15").Value = "Colaborador: ";
                var lineA15Range = worksheet.Range("A15:B15");
                lineA15Range.Merge().Style.Font.SetFontColor(XLColor.White).Font.SetBold(); ;
                lineA15Range.Style.Fill.BackgroundColor = XLColor.FromArgb(0, 153, 153);
                lineA15Range.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                lineA15Range.Style.Border.TopBorder = XLBorderStyleValues.Thin;
                lineA15Range.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                lineA15Range.Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                lineA15Range.Style.Border.RightBorder = XLBorderStyleValues.Thin;
                lineA15Range.Style.Border.TopBorder = XLBorderStyleValues.Thin;

                worksheet.Cell("C15").Value = userName;
                var lineC15Range = worksheet.Range("C15:F15");
                lineC15Range.Merge().Style.Font.SetFontColor(XLColor.White).Font.SetBold(); ;
                lineC15Range.Style.Fill.BackgroundColor = XLColor.FromArgb(0, 153, 153);
                lineC15Range.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                lineC15Range.Style.Border.TopBorder = XLBorderStyleValues.Thin;
                lineC15Range.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                lineC15Range.Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                lineC15Range.Style.Border.RightBorder = XLBorderStyleValues.Thin;
                lineC15Range.Style.Border.TopBorder = XLBorderStyleValues.Thin;
                #endregion

                await using var memory = new MemoryStream();
                workbook.SaveAs(memory);

                return File(memory.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ch.xlsx");
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
