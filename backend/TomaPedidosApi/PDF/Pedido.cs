using DTOEntidades;
using Microsoft.AspNetCore.Hosting;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TomaPedidosApi.PDF
{
    public class Pedido
    {
        private readonly IWebHostEnvironment _host;

        public Pedido()
        {
        }

        public byte[] Reporte(DTOPedidoVentaCab Pedido)
        {
            byte[] data = Document.Create(document =>
            {
                double totalSOL = 0;
                double totalUSD = 0;

                document.Page(page =>
                {
                    page.Margin(30);

                    page.Header().ShowOnce().Row(row =>
                    {
                        //var rutaImagen = Path.Combine(_host.WebRootPath, "images/VisualStudio.png");
                        //byte[] imageData = System.IO.File.ReadAllBytes(rutaImagen);

                        //row.ConstantItem(140).Height(60).Placeholder();
                        //row.ConstantItem(150).Image(imageData);

                        //row.ConstantItem(350).Column(col =>
                        //{
                        //    col.Item().AlignCenter().Text("PEDIDO").Bold().FontSize(12);
                        //});

                        row.RelativeItem().Column(col =>
                        {
                            col.Item().AlignCenter().Text(Pedido.Compania).Bold().FontSize(14);
                            col.Item().AlignCenter().Text(Pedido.CompaniaDir).FontSize(9);
                            col.Item().AlignCenter().Text(Pedido.CompaniaTel).FontSize(9);
                            col.Item().AlignCenter().Text(Pedido.CompaniaMail).FontSize(9);

                        });

                        row.RelativeItem().Column(col =>
                        {
                            col.Item().Border(1).BorderColor("#257272")
                            .AlignCenter().Text($"RUC {Pedido.CompaniaRuc}");

                            col.Item().Background("#257272").Border(1)
                            .BorderColor("#257272").AlignCenter()
                            .Text("PEDIDO VENTA").FontColor("#fff");

                            col.Item().Border(1).BorderColor("#257272").
                            AlignCenter().Text(Pedido.NroPedido);


                        });
                    });

                    page.Content().PaddingVertical(10).Column(col1 =>
                    {
                        col1.Item().Column(col2 =>
                        {
                            col2.Item().Text("Datos del cliente").Underline().Bold();

                            col2.Item().Text(txt =>
                            {
                                txt.Span("Nombre: ").SemiBold().FontSize(10);
                                txt.Span(Pedido.NomCliente).FontSize(10);
                            });

                            col2.Item().Text(txt =>
                            {
                                txt.Span("R.U.C: ").SemiBold().FontSize(10);
                                txt.Span(Pedido.RucCliente).FontSize(10);
                            });

                            col2.Item().Text(txt =>
                            {
                                txt.Span("Direccion: ").SemiBold().FontSize(10);
                                txt.Span(Pedido.Direccion).FontSize(10);
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

                            });

                            tabla.Header(header =>
                            {
                                header.Cell().Background("#257272")
                                .Padding(2).Text("#").FontColor("#fff");

                                header.Cell().Background("#257272")
                               .Padding(2).Text("Producto").FontColor("#fff");

                                header.Cell().Background("#257272")
                               .Padding(2).Text("Cantidad").FontColor("#fff");

                                header.Cell().Background("#257272")
                               .Padding(2).Text("Unidad").FontColor("#fff");

                                header.Cell().Background("#257272")
                               .Padding(2).Text("Precio").FontColor("#fff");

                                header.Cell().Background("#257272")
                               .Padding(2).Text("Precio Total").FontColor("#fff");
                            });

                            int i = 0;

                            foreach (DTOPedidoVentaDet item in Pedido.ListaDetalle)
                            {
                                totalUSD += item.PrecioTotal;

                                tabla.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9")
                                .Padding(2).Text(i.ToString()).FontSize(10);

                                tabla.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9")
                                .Padding(2).Text(item.Descripcion).FontSize(10);

                                tabla.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9")
                                .Padding(2).AlignRight().Text(item.Cantidad.ToString()).FontSize(10);

                                tabla.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9")
                                .Padding(2).AlignCenter().Text(item.Unidad).FontSize(10);

                                tabla.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9")
                                .Padding(2).AlignRight().Text(item.PrecioUnit.ToString("N2")).FontSize(10);

                                tabla.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9")
                                .Padding(2).AlignRight().Text(item.PrecioTotal.ToString("N2")).FontSize(10);

                                i++;
                            }
                        });

                        col1.Item().AlignRight().Text($"Total: { totalUSD.ToString("N2") }").FontSize(12);

                        col1.Spacing(10);
                    });


                    page.Footer()
                    .AlignRight()
                    .Text(txt =>
                    {
                        txt.Span("Pagina ").FontSize(10);
                        txt.CurrentPageNumber().FontSize(10);
                        txt.Span(" de ").FontSize(10);
                        txt.TotalPages().FontSize(10);
                    });
                });
            }).GeneratePdf();

            //Stream stream = new MemoryStream(data);
            return data;
        }
    }
}
