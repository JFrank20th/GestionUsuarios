<%@ Page Language="C#" AutoEventWireup="true" Async="true" CodeBehind="UsuarioEditar.aspx.cs" Inherits="SistemaUsuarios.Web.Pages.UsuarioEditar" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Editar Usuario</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet"/>
    <style>
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
                    <li class="breadcrumb-item active" aria-current="page">Editor de Usuarios</li>
                </ol>
            </nav>

            <h2 class="mb-4">Editar Usuario</h2>

            <div class="mb-3" hidden="hidden">
                <asp:Label ID="lblId" runat="server" Text="ID:" CssClass="form-label" />
                <asp:Label ID="lblIdValor" runat="server" CssClass="form-control-plaintext" />
            </div>

            <div class="mb-3">
                <asp:Label ID="lblNombre" runat="server" Text="Nombre:" CssClass="form-label" AssociatedControlID="txtNombre" />
                <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" />
            </div>

            <div class="mb-3">
                <asp:Label ID="lblFechaNacimiento" runat="server" Text="Fecha de Nacimiento:" CssClass="form-label" AssociatedControlID="txtFechaNacimiento" />
                <asp:TextBox ID="txtFechaNacimiento" runat="server" TextMode="Date" CssClass="form-control" />
            </div>

            <div class="mb-3">
                <asp:Label ID="lblSexo" runat="server" Text="Sexo:" CssClass="form-label" AssociatedControlID="ddlSexo" />
                <asp:DropDownList ID="ddlSexo" runat="server" CssClass="form-select">
                    <asp:ListItem Text="Seleccione" Value="" />
                    <asp:ListItem Text="Masculino" Value="M" />
                    <asp:ListItem Text="Femenino" Value="F" />
                </asp:DropDownList>
            </div>

            <asp:Button ID="btnActualizar" runat="server" Text="Actualizar" OnClick="btnActualizar_Click" CssClass="btn btn-success" OnClientClick="return confirmarEdicion();"/>
            <asp:Label ID="lblMensaje" runat="server" ForeColor="Green" CssClass="d-block mt-2" />

            <div class="mt-3">
                <asp:HyperLink ID="hlVolver" NavigateUrl="~/Pages/UsuarioConsulta.aspx" runat="server" CssClass="btn btn-secondary">Volver</asp:HyperLink>
            </div>
        </div>
    </form>

    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js"></script>
    <script type="text/javascript">
        function confirmarEdicion() {
            return confirm('¿Está seguro que desea Editar este usuario?');
        }
    </script>
</body>
</html>
