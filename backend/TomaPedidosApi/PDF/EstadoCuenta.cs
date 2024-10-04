using DTOEntidades;
using Microsoft.AspNetCore.Hosting;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TomaPedidosApi.PDF
{
    public class EstadoCuenta
    {
        private readonly IWebHostEnvironment _host;

        public EstadoCuenta()
        {
        }

        public byte[] ReporteEECC(List<DTOEstadoCuenta> oLista)
        {
            byte[] data = Document.Create(document =>
            {
                double totalSOL = 0;
                double totalUSD = 0;
                double importeVenc = 0;
                //double importeDisp = 0;
                double importeVencUSD = 0;
                double importeDispUSD = 0;

                foreach (DTOEstadoCuenta item in oLista)
                {
                    if(item.DiasVencidos > 0)
                    {
                        importeVenc += item.Saldo;
                        importeVencUSD += item.SaldoUsd;
                    }

                    totalSOL += item.Saldo;
                    totalUSD += item.SaldoUsd;
                }

                importeDispUSD = oLista[0].LineaCredito - (totalUSD + Math.Round((totalSOL / oLista[0].TipoCambio), 2));
                importeVencUSD += Math.Round((importeVenc / oLista[0].TipoCambio), 2);
                totalUSD += Math.Round((totalSOL / oLista[0].TipoCambio), 2);

                document.Page(page =>
                {
                    page.Margin(30);

                    page.Header().ShowOnce().Row(row =>
                    {
                        //var rutaImagen = Path.Combine(_host.WebRootPath, "images/VisualStudio.png");
                        //byte[] imageData = System.IO.File.ReadAllBytes(rutaImagen);

                        //row.ConstantItem(140).Height(60).Placeholder();
                        //row.ConstantItem(150).Image(imageData);

                        row.ConstantItem(350).Column(col =>
                        {
                            col.Item().AlignCenter().Text("ESTADO DE CUENTA").Bold().FontSize(12);
                        });

                        row.RelativeItem().Column(col =>
                        {
                            col.Item().AlignCenter().Text(oLista[0].Compania).Bold().FontSize(14);
                            col.Item().AlignCenter().Text(oLista[0].CompaniaDir).FontSize(9);
                            col.Item().AlignCenter().Text(oLista[0].CompaniaTel).FontSize(9);
                            col.Item().AlignCenter().Text(oLista[0].CompaniaMail).FontSize(9);

                        });

                        //row.RelativeItem().Column(col =>
                        //{
                        //    col.Item().Border(1).BorderColor("#257272")
                        //    .AlignCenter().Text("RUC 21312312312");

                        //    col.Item().Background("#257272").Border(1)
                        //    .BorderColor("#257272").AlignCenter()
                        //    .Text("Boleta de venta").FontColor("#fff");

                        //    col.Item().Border(1).BorderColor("#257272").
                        //    AlignCenter().Text("B0001 - 234");


                        //});
                    });

                    page.Content().PaddingVertical(10).Column(col1 =>
                    {
                        col1.Item().Column(col2 =>
                        {
                            col2.Item().Text("Datos del cliente").Underline().Bold();

                            col2.Item().Text(txt =>
                            {
                                txt.Span("Nombre: ").SemiBold().FontSize(10);
                                txt.Span(oLista[0].NomCliente).FontSize(10);
                            });

                            col2.Item().Text(txt =>
                            {
                                txt.Span("R.U.C: ").SemiBold().FontSize(10);
                                txt.Span(oLista[0].CodCliente).FontSize(10);
                            });

                            col2.Item().Text(txt =>
                            {
                                txt.Span("Direccion: ").SemiBold().FontSize(10);
                                txt.Span(oLista[0].Direccion).FontSize(10);
                            });

                            col2.Item().Text(txt =>
                            {
                                txt.Span("Línea de Crédito: USD").SemiBold().FontSize(10);
                                txt.Span(oLista[0].LineaCredito.ToString("N2")).FontSize(10);
                            });

                            col2.Item().Text(txt =>
                            {
                                txt.Span("Importe Vencido: USD").SemiBold().FontSize(10);
                                txt.Span(importeVencUSD.ToString("N2")).FontSize(10);
                            });

                            col2.Item().Text(txt =>
                            {
                                txt.Span("Línea Utilizado: USD").SemiBold().FontSize(10);
                                txt.Span(totalUSD.ToString("N2")).FontSize(10);
                            });
                            
                            col2.Item().Text(txt =>
                            {
                                txt.Span("Línea Disponible: USD").SemiBold().FontSize(10);
                                txt.Span(importeDispUSD.ToString("N2")).FontSize(10);
                            });
                        });

                        col1.Item().LineHorizontal(0.5f);

                        col1.Item().Table(tabla =>
                        {
                            tabla.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();

                            });

                            tabla.Header(header =>
                            {
                                header.Cell().Background("#257272").Padding(2).Text("Tipo Doc").FontColor("#fff");

                                header.Cell().Background("#257272").Padding(2).Text("N° Doc.").FontColor("#fff");

                                header.Cell().Background("#257272").Padding(2).Text("F. Venc.").FontColor("#fff");

                                header.Cell().Background("#257272").Padding(2).Text("Moneda").FontColor("#fff");

                                header.Cell().Background("#257272").Padding(2).Text("Imp. SOL").FontColor("#fff");

                                header.Cell().Background("#257272").Padding(2).Text("Imp. USD").FontColor("#fff");

                                header.Cell().Background("#257272").Padding(2).Text("Dias Venc.").FontColor("#fff");
                            });

                            foreach (DTOEstadoCuenta item in oLista)
                            {
                                tabla.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9").Padding(2).Text(item.TipoDoc).FontSize(9);

                                tabla.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9").Padding(2).Text(item.NroDocumento).FontSize(9);

                                tabla.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9").Padding(2).Text(item.FecVencimiento.ToShortDateString()).FontSize(9);

                                tabla.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9").Padding(2).AlignCenter().Text(item.Moneda).FontSize(9);

                                tabla.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9").Padding(2).AlignRight().Text(item.Saldo.ToString("N2")).FontSize(9);

                                tabla.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9").Padding(2).AlignRight().Text(item.SaldoUsd.ToString("N2")).FontSize(9);

                                tabla.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9").Padding(2).AlignRight().Text(item.DiasVencidos.ToString()).FontSize(9);
                            }
                        });

                        col1.Item().Column(col2 =>
                        {
                            col2.Item().Text(txt =>
                            {
                                txt.Span($"Total: USD {totalUSD.ToString("N2")}").SemiBold().FontSize(10);
                                txt.Span($"                                    ").SemiBold().FontSize(10);
                                txt.Span($"Total: SOL {totalSOL.ToString("N2")}").SemiBold().FontSize(10);
                            });
                        });

                        col1.Spacing(10);
                    });

                    page.Footer()
                    .AlignRight()
                    .Text(txt =>
                    {
                        txt.Span("Pagina ").FontSize(8);
                        txt.CurrentPageNumber().FontSize(8);
                        txt.Span(" de ").FontSize(8);
                        txt.TotalPages().FontSize(8);
                    });
                });
            }).GeneratePdf();

            //Stream stream = new MemoryStream(data);
            return data;
        }
    }
}
