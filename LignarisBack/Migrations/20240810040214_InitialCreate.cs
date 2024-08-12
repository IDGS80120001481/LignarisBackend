using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LignarisBack.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Fullname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "materia_prima",
                columns: table => new
                {
                    id_materia_prima = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    tipo_medida = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    cantidad_minima = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__materia___1BCDA74B50B9D580", x => x.id_materia_prima);
                });

            migrationBuilder.CreateTable(
                name: "persona",
                columns: table => new
                {
                    id_persona = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    apellido_paterno = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    apellido_materno = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    telefono = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    direccion = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__persona__228148B05E9E6BC2", x => x.id_persona);
                });

            migrationBuilder.CreateTable(
                name: "receta",
                columns: table => new
                {
                    id_receta = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    foto = table.Column<string>(type: "text", nullable: true),
                    tamanio = table.Column<int>(type: "int", nullable: true),
                    precio_unitario = table.Column<double>(type: "float", nullable: true),
                    descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    estatus = table.Column<int>(type: "int", nullable: true, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__receta__11DB53ABB651B1C6", x => x.id_receta);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "registro_sesiones",
                columns: table => new
                {
                    id_registro = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fecha_hora_accion = table.Column<DateTime>(type: "datetime", nullable: true),
                    estatus_sesion = table.Column<int>(type: "int", nullable: true),
                    id_usuario = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__registro__48155C1F728AC443", x => x.id_registro);
                    table.ForeignKey(
                        name: "fk_id_usuario_registro",
                        column: x => x.id_usuario,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "cliente",
                columns: table => new
                {
                    id_cliente = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_persona = table.Column<int>(type: "int", nullable: false),
                    id_usuario = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__cliente__677F38F5A3993B3C", x => x.id_cliente);
                    table.ForeignKey(
                        name: "FK_id_persona_Cliente",
                        column: x => x.id_persona,
                        principalTable: "persona",
                        principalColumn: "id_persona");
                    table.ForeignKey(
                        name: "FK_id_usuario_Cliente",
                        column: x => x.id_usuario,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "empleado",
                columns: table => new
                {
                    id_empleado = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    puesto = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    id_persona = table.Column<int>(type: "int", nullable: false),
                    id_usuario = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__empleado__88B513943E6A5F08", x => x.id_empleado);
                    table.ForeignKey(
                        name: "FK_id_persona_empleado",
                        column: x => x.id_persona,
                        principalTable: "persona",
                        principalColumn: "id_persona");
                    table.ForeignKey(
                        name: "FK_id_usuario_empleado",
                        column: x => x.id_usuario,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "proveedor",
                columns: table => new
                {
                    id_proveedor = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_persona = table.Column<int>(type: "int", nullable: false),
                    estatus = table.Column<int>(type: "int", nullable: true, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__proveedo__8D3DFE289762FB9F", x => x.id_proveedor);
                    table.ForeignKey(
                        name: "FK_id_persona_proveedor",
                        column: x => x.id_persona,
                        principalTable: "persona",
                        principalColumn: "id_persona");
                });

            migrationBuilder.CreateTable(
                name: "receta_detalle",
                columns: table => new
                {
                    id_receta_detalle = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_receta = table.Column<int>(type: "int", nullable: true),
                    id_materia_prima = table.Column<int>(type: "int", nullable: false),
                    cantidad = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__receta_d__F085822D7618FBF1", x => x.id_receta_detalle);
                    table.ForeignKey(
                        name: "fk_id_materia_prima_receta_detalle",
                        column: x => x.id_materia_prima,
                        principalTable: "materia_prima",
                        principalColumn: "id_materia_prima",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_id_receta_receta_detalle",
                        column: x => x.id_receta,
                        principalTable: "receta",
                        principalColumn: "id_receta");
                });

            migrationBuilder.CreateTable(
                name: "carritocompras",
                columns: table => new
                {
                    id_carritocompras = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cantidad = table.Column<int>(type: "int", nullable: false),
                    IdRecetas = table.Column<int>(type: "int", nullable: false),
                    IdCliente = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__cliente__677F38F5A3993B9J", x => x.id_carritocompras);
                    table.ForeignKey(
                        name: "FK_carritocompras_cliente_IdCliente",
                        column: x => x.IdCliente,
                        principalTable: "cliente",
                        principalColumn: "id_cliente",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_carritocompras_receta_IdRecetas",
                        column: x => x.IdRecetas,
                        principalTable: "receta",
                        principalColumn: "id_receta",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "venta",
                columns: table => new
                {
                    id_venta = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_empleado = table.Column<int>(type: "int", nullable: true),
                    id_cliente = table.Column<int>(type: "int", nullable: true),
                    estatus = table.Column<int>(type: "int", nullable: true),
                    fecha_venta = table.Column<DateOnly>(type: "date", nullable: true),
                    total = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__venta__459533BF9A62FD80", x => x.id_venta);
                    table.ForeignKey(
                        name: "fk_id_cliente_compra",
                        column: x => x.id_cliente,
                        principalTable: "cliente",
                        principalColumn: "id_cliente");
                    table.ForeignKey(
                        name: "fk_id_empleado_venta",
                        column: x => x.id_empleado,
                        principalTable: "empleado",
                        principalColumn: "id_empleado");
                });

            migrationBuilder.CreateTable(
                name: "compra",
                columns: table => new
                {
                    id_compra = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_empleado = table.Column<int>(type: "int", nullable: false),
                    Id_proveedor = table.Column<int>(type: "int", nullable: false),
                    fecha_compra = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__compra__C4BAA6044664E5EF", x => x.id_compra);
                    table.ForeignKey(
                        name: "fk_id_empleado_compra",
                        column: x => x.Id_empleado,
                        principalTable: "empleado",
                        principalColumn: "id_empleado");
                    table.ForeignKey(
                        name: "fk_id_proveedor_compra",
                        column: x => x.Id_proveedor,
                        principalTable: "proveedor",
                        principalColumn: "id_proveedor");
                });

            migrationBuilder.CreateTable(
                name: "materia_proveedor_intermedia",
                columns: table => new
                {
                    id_materia_proveedor = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_materia_prima = table.Column<int>(type: "int", nullable: false),
                    id_proveedor = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__materia___C380E4DD145ECA3E", x => x.id_materia_proveedor);
                    table.ForeignKey(
                        name: "FK_id_materia_prima",
                        column: x => x.id_materia_prima,
                        principalTable: "materia_prima",
                        principalColumn: "id_materia_prima");
                    table.ForeignKey(
                        name: "FK_id_proveedor",
                        column: x => x.id_proveedor,
                        principalTable: "proveedor",
                        principalColumn: "id_proveedor");
                });

            migrationBuilder.CreateTable(
                name: "produccion",
                columns: table => new
                {
                    id_produccion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_venta = table.Column<int>(type: "int", nullable: true),
                    id_empleado = table.Column<int>(type: "int", nullable: true),
                    fecha_produccion = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__producci__9EBBA4330F7A26BC", x => x.id_produccion);
                    table.ForeignKey(
                        name: "fk_id_empleado_produccion",
                        column: x => x.id_empleado,
                        principalTable: "empleado",
                        principalColumn: "id_empleado");
                    table.ForeignKey(
                        name: "fk_id_solicitud_produccion",
                        column: x => x.id_venta,
                        principalTable: "venta",
                        principalColumn: "id_venta");
                });

            migrationBuilder.CreateTable(
                name: "venta_detalle",
                columns: table => new
                {
                    id_venta_detalle = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_venta = table.Column<int>(type: "int", nullable: true),
                    id_receta = table.Column<int>(type: "int", nullable: true),
                    cantidad = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__venta_de__7AA8F41B68D795FB", x => x.id_venta_detalle);
                    table.ForeignKey(
                        name: "fK_id_venta_detalle",
                        column: x => x.id_venta,
                        principalTable: "venta",
                        principalColumn: "id_venta");
                    table.ForeignKey(
                        name: "fk_id_receta",
                        column: x => x.id_receta,
                        principalTable: "receta",
                        principalColumn: "id_receta");
                });

            migrationBuilder.CreateTable(
                name: "compra_detalle",
                columns: table => new
                {
                    id_compra_detalle = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_compra = table.Column<int>(type: "int", nullable: true),
                    id_materia_prima = table.Column<int>(type: "int", nullable: true),
                    precio_unitario = table.Column<int>(type: "int", nullable: true),
                    cantidad = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    num_lote = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    fecha_caducidad = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__compra_d__C08AA0066404E084", x => x.id_compra_detalle);
                    table.ForeignKey(
                        name: "fk_id_compra_detalle",
                        column: x => x.id_compra,
                        principalTable: "compra",
                        principalColumn: "id_compra");
                    table.ForeignKey(
                        name: "fk_id_detalle_compra_materia_prima",
                        column: x => x.id_materia_prima,
                        principalTable: "materia_prima",
                        principalColumn: "id_materia_prima");
                });

            migrationBuilder.CreateTable(
                name: "inventario",
                columns: table => new
                {
                    id_inventario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_compra_detalle = table.Column<int>(type: "int", nullable: true),
                    cantidad_disponible = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    estatus = table.Column<int>(type: "int", nullable: true, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__inventar__013AEB511138B038", x => x.id_inventario);
                    table.ForeignKey(
                        name: "fk_id_compra_detalle_inventario",
                        column: x => x.id_compra_detalle,
                        principalTable: "compra_detalle",
                        principalColumn: "id_compra_detalle");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_carritocompras_IdCliente",
                table: "carritocompras",
                column: "IdCliente");

            migrationBuilder.CreateIndex(
                name: "IX_carritocompras_IdRecetas",
                table: "carritocompras",
                column: "IdRecetas");

            migrationBuilder.CreateIndex(
                name: "IX_cliente_id_persona",
                table: "cliente",
                column: "id_persona");

            migrationBuilder.CreateIndex(
                name: "IX_cliente_id_usuario",
                table: "cliente",
                column: "id_usuario");

            migrationBuilder.CreateIndex(
                name: "IX_compra_Id_empleado",
                table: "compra",
                column: "Id_empleado");

            migrationBuilder.CreateIndex(
                name: "IX_compra_Id_proveedor",
                table: "compra",
                column: "Id_proveedor");

            migrationBuilder.CreateIndex(
                name: "IX_compra_detalle_id_compra",
                table: "compra_detalle",
                column: "id_compra");

            migrationBuilder.CreateIndex(
                name: "IX_compra_detalle_id_materia_prima",
                table: "compra_detalle",
                column: "id_materia_prima");

            migrationBuilder.CreateIndex(
                name: "IX_empleado_id_persona",
                table: "empleado",
                column: "id_persona");

            migrationBuilder.CreateIndex(
                name: "IX_empleado_id_usuario",
                table: "empleado",
                column: "id_usuario");

            migrationBuilder.CreateIndex(
                name: "IX_inventario_id_compra_detalle",
                table: "inventario",
                column: "id_compra_detalle");

            migrationBuilder.CreateIndex(
                name: "IX_materia_proveedor_intermedia_id_materia_prima",
                table: "materia_proveedor_intermedia",
                column: "id_materia_prima");

            migrationBuilder.CreateIndex(
                name: "IX_materia_proveedor_intermedia_id_proveedor",
                table: "materia_proveedor_intermedia",
                column: "id_proveedor");

            migrationBuilder.CreateIndex(
                name: "IX_produccion_id_empleado",
                table: "produccion",
                column: "id_empleado");

            migrationBuilder.CreateIndex(
                name: "IX_produccion_id_venta",
                table: "produccion",
                column: "id_venta");

            migrationBuilder.CreateIndex(
                name: "IX_proveedor_id_persona",
                table: "proveedor",
                column: "id_persona");

            migrationBuilder.CreateIndex(
                name: "IX_receta_detalle_id_materia_prima",
                table: "receta_detalle",
                column: "id_materia_prima");

            migrationBuilder.CreateIndex(
                name: "IX_receta_detalle_id_receta",
                table: "receta_detalle",
                column: "id_receta");

            migrationBuilder.CreateIndex(
                name: "IX_registro_sesiones_id_usuario",
                table: "registro_sesiones",
                column: "id_usuario");

            migrationBuilder.CreateIndex(
                name: "IX_venta_id_cliente",
                table: "venta",
                column: "id_cliente");

            migrationBuilder.CreateIndex(
                name: "IX_venta_id_empleado",
                table: "venta",
                column: "id_empleado");

            migrationBuilder.CreateIndex(
                name: "IX_venta_detalle_id_receta",
                table: "venta_detalle",
                column: "id_receta");

            migrationBuilder.CreateIndex(
                name: "IX_venta_detalle_id_venta",
                table: "venta_detalle",
                column: "id_venta");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "carritocompras");

            migrationBuilder.DropTable(
                name: "inventario");

            migrationBuilder.DropTable(
                name: "materia_proveedor_intermedia");

            migrationBuilder.DropTable(
                name: "produccion");

            migrationBuilder.DropTable(
                name: "receta_detalle");

            migrationBuilder.DropTable(
                name: "registro_sesiones");

            migrationBuilder.DropTable(
                name: "venta_detalle");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "compra_detalle");

            migrationBuilder.DropTable(
                name: "venta");

            migrationBuilder.DropTable(
                name: "receta");

            migrationBuilder.DropTable(
                name: "compra");

            migrationBuilder.DropTable(
                name: "materia_prima");

            migrationBuilder.DropTable(
                name: "cliente");

            migrationBuilder.DropTable(
                name: "empleado");

            migrationBuilder.DropTable(
                name: "proveedor");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "persona");
        }
    }
}
