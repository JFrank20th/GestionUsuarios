<%@ Page Language="C#" AutoEventWireup="true" Async="true" CodeBehind="UsuarioConsulta.aspx.cs" Inherits="SistemaUsuarios.Web.Pages.UsuarioConsulta" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Listado de Usuarios</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet"/>
    <style>
        .gridview thead tr th,
        .gridview tbody tr td {
            vertical-align: middle;
            text-align: center;
        }
        /* Estilos para la paginación */
        .pagination-container {
            display: flex;
            justify-content: space-between;
            align-items: center;
            margin-top: 20px;
        }

        .gridview .pagination {
            display: flex;
            list-style: none;
            padding: 0;
            margin: 0;
        }

        .gridview .pagination td {
            padding: 0;
        }

        .gridview .pagination a, 
        .gridview .pagination span {
            padding: 6px 12px;
            margin: 0 3px;
            border: 1px solid #ddd;
            text-decoration: none;
            color: #0d6efd;
            border-radius: 4px;
            display: inline-block;
        }

        .gridview .pagination a:hover {
            background-color: #f5f5f5;
        }

        .gridview .pagination .active a {
            background-color: #0d6efd;
            color: white;
            border-color: #0d6efd;
        }

        .gridview .pagination .disabled a {
            color: #ccc;
            pointer-events: none;
        }

        .breadcrumb {
            padding: 0.5rem 0;
            background-color: transparent;
            font-size: 0.9rem;
        }
        .breadcrumb-item + .breadcrumb-item::before {
            content: ">";
            padding: 0 5px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container mt-4">

            <nav aria-label="breadcrumb" class="mb-4">
                <ol class="breadcrumb">
                    <li class="breadcrumb-item"><a href="/Pages/UsuarioConsulta.aspx">Inicio</a></li>
                    <li class="breadcrumb-item active" aria-current="page">Listado de Usuarios</li>
                </ol>
            </nav>

            <h2 class="mb-4">Listado de Usuarios</h2>
            <div class="d-flex justify-content-between align-items-center mb-3">
                <asp:HyperLink ID="HyperLink1" NavigateUrl="~/Pages/Usuario.aspx" runat="server" CssClass="btn btn-primary">
                    Nuevo Usuario
                </asp:HyperLink>

                <asp:Button ID="Button1" runat="server" Text="Descargar Excel"
                    CssClass="btn btn-success"
                    OnClick="btnDescargar_Click" />
            </div>
            <asp:GridView ID="gvUsuarios" runat="server"
                CssClass="table table-bordered table-striped gridview"
                AutoGenerateColumns="False" DataKeyNames="Id"
                OnRowCommand="gvUsuarios_RowCommand"
                AllowPaging="true" PageSize="10"
                OnPageIndexChanging="gvUsuarios_PageIndexChanging"
                PagerStyle-CssClass="pagination"
                PagerSettings-Mode="NumericFirstLast"
                PagerSettings-FirstPageText="Primera"
                PagerSettings-LastPageText="Última"
                PagerSettings-Position="Bottom"
                PagerSettings-PageButtonCount="5">
                <HeaderStyle CssClass="table-dark" />
                <Columns>
                    <asp:BoundField DataField="Id" HeaderText="ID" />
                    <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                    <asp:BoundField DataField="FechaNacimiento" HeaderText="Fecha Nacimiento" DataFormatString="{0:yyyy-MM-dd}" />
                    <asp:BoundField DataField="Sexo" HeaderText="Sexo" />

                    <asp:TemplateField HeaderText="Acciones">
                        <ItemTemplate>
                            <div class="d-flex justify-content-center gap-2">
                                <asp:Button ID="btnEditar" runat="server" Text="Editar" CommandName="Editar"
                                          CommandArgument='<%# Container.DataItemIndex %>' CssClass="btn btn-sm btn-warning" />
                                <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CommandName="Eliminar"
                                      CommandArgument='<%# Container.DataItemIndex %>' 
                                      CssClass="btn btn-sm btn-danger"
                                      OnClientClick="return confirmarEliminacion();" />
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>

            <div class="d-flex justify-content-center mt-3">
                <asp:Button ID="btnAnterior" runat="server" Text="Anterior" 
                    CssClass="btn btn-outline-primary me-2" 
                    OnClick="btnAnterior_Click" 
                    Enabled="false" />
        
                <asp:Button ID="btnSiguiente" runat="server" Text="Siguiente" 
                    CssClass="btn btn-outline-primary" 
                    OnClick="btnSiguiente_Click" 
                    Enabled="false" />
            </div>


            <asp:Label ID="lblMensaje" runat="server" ForeColor="Green" CssClass="d-block mt-2" />
            <div class="text-center mt-2">
                <asp:Label ID="lblPaginaActual" runat="server" CssClass="text-muted"></asp:Label>
            </div>
        </div>
    </form>

    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js"></script>
    <script type="text/javascript">
        function confirmarEliminacion() {
            return confirm('¿Está seguro que desea eliminar este usuario?');
        }
    </script>
</body>
</html>
