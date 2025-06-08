using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading.Tasks;
using OfficeOpenXml;


namespace SistemaUsuarios.Web.Pages
{
    public partial class UsuarioConsulta : System.Web.UI.Page
    {
        private string apiBaseUrl = "https://localhost:44386/"; // Reemplazar con URL API

        protected async void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                await CargarUsuarios();
        }


        private async Task CargarUsuarios()
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiBaseUrl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("api/usuario/consultar");
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    var usuarios = JsonConvert.DeserializeObject<List<Models.Usuario>>(json);
                    gvUsuarios.DataSource = usuarios;
                    gvUsuarios.DataBind();
                }
            }
        }

        protected async void gvUsuarios_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Eliminar" || e.CommandName == "Editar")
            {
                GridViewRow row = (GridViewRow)((Control)e.CommandSource).NamingContainer;

                int id = Convert.ToInt32(gvUsuarios.DataKeys[row.RowIndex].Value);

                if (e.CommandName == "Eliminar")
                {
                    using (HttpClient client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(apiBaseUrl);
                        HttpResponseMessage response = await client.DeleteAsync($"api/usuario/eliminar/{id}");

                        if (response.IsSuccessStatusCode)
                        {
                            lblMensaje.Text = "Usuario eliminado.";
                            await CargarUsuarios();
                        }
                    }
                }
                else if (e.CommandName == "Editar")
                {
                    Response.Redirect($"UsuarioEditar.aspx?id={id}", false);
                    Context.ApplicationInstance.CompleteRequest();
                }
            }
        }

        protected void gvUsuarios_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvUsuarios.PageIndex = e.NewPageIndex;
            Task.Run(() => CargarUsuarios()).Wait();
        }

        protected void btnDescargar_Click(object sender, EventArgs e)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiBaseUrl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = client.GetAsync("api/usuario/consultar").Result;

                if (response.IsSuccessStatusCode)
                {
                    string json = response.Content.ReadAsStringAsync().Result;
                    var usuarios = JsonConvert.DeserializeObject<List<Models.Usuario>>(json);

                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                    using (ExcelPackage pck = new ExcelPackage())
                    {
                        ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Usuarios");

                        ws.Cells["A1"].Value = "ID";
                        ws.Cells["B1"].Value = "Nombre";
                        ws.Cells["C1"].Value = "Fecha Nacimiento";
                        ws.Cells["D1"].Value = "Sexo";

                        int row = 2;
                        foreach (var u in usuarios)
                        {
                            ws.Cells[row, 1].Value = u.Id;
                            ws.Cells[row, 2].Value = u.Nombre;
                            ws.Cells[row, 3].Value = u.FechaNacimiento.ToString("yyyy-MM-dd");
                            ws.Cells[row, 4].Value = u.Sexo;
                            row++;
                        }

                        HttpContext.Current.Response.Clear();
                        HttpContext.Current.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=Usuarios.xlsx");
                        HttpContext.Current.Response.BinaryWrite(pck.GetAsByteArray());
                        HttpContext.Current.Response.End();
                    }
                }
            }
        }



        protected void Page_PreRender(object sender, EventArgs e)
        {
            // Habilitar/deshabilitar botones de paginación
            btnAnterior.Enabled = gvUsuarios.PageIndex > 0;
            btnSiguiente.Enabled = gvUsuarios.PageIndex < gvUsuarios.PageCount - 1;

            // Mostrar información de paginación
            lblPaginaActual.Text = $"Página {gvUsuarios.PageIndex + 1} de {gvUsuarios.PageCount}";
        }

        protected void btnAnterior_Click(object sender, EventArgs e)
        {
            if (gvUsuarios.PageIndex > 0)
            {
                gvUsuarios.PageIndex--;
                Task.Run(() => CargarUsuarios()).Wait();
            }
        }

        protected void btnSiguiente_Click(object sender, EventArgs e)
        {
            if (gvUsuarios.PageIndex < gvUsuarios.PageCount - 1)
            {
                gvUsuarios.PageIndex++;
                Task.Run(() => CargarUsuarios()).Wait();
            }
        }


    }
}