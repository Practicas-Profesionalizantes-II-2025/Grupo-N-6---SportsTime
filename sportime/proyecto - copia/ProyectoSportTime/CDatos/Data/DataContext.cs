using Microsoft.EntityFrameworkCore;
using Shared.Entidades;
using System.Collections.Generic;

namespace CDatos.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
        : base(options)
        {
        }
        public DataContext() { }
        public DbSet<Canchas> Canchas { get; set; }
        public DbSet<Deportes> Deportes { get; set; }
        public DbSet<Productos> Productos { get; set; }
        public DbSet<Proveedores> Proveedores { get; set; }
        public DbSet<Turnos> Turnos { get; set; }
        public DbSet<TurnoProducto> TurnoProductos { get; set; }
        public DbSet<Usuarios> Usuarios { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=PCLEO\\SQLEXPRESS;Initial Catalog=SportTime;Integrated Security=True;TrustServerCertificate=true;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Administradores
            //modelBuilder.Entity<Administrador>().HasData(
            //    new Administrador { Admin_ID = 1, Nombre = "Admin1", Email = "admin1@test.com", Contraseña = "1234", IsSuperAdmin = true },
            //    new Administrador { Admin_ID = 2, Nombre = "Admin2", Email = "admin2@test.com", Contraseña = "1234", IsSuperAdmin = false },
            //    new Administrador { Admin_ID = 3, Nombre = "Admin3", Email = "admin3@test.com", Contraseña = "1234", IsSuperAdmin = false },
            //    new Administrador { Admin_ID = 4, Nombre = "Admin4", Email = "admin4@test.com", Contraseña = "1234", IsSuperAdmin = true }
            //);

            // Clientes
            //modelbuilder.entity<clientes>().hasdata(
            //    new clientes { cliente_id = 1, nombre = "juan perez", numerotelefono = 111111111 },
            //    new clientes { cliente_id = 2, nombre = "ana gomez", numerotelefono = 222222222 },
            //    new clientes { cliente_id = 3, nombre = "carlos lopez", numerotelefono = 333333333 },
            //    new clientes { cliente_id = 4, nombre = "maria diaz", numerotelefono = 444444444 }
            //);

            // Deportes
            modelBuilder.Entity<Deportes>().HasData(
                new Deportes { Deporte_ID = 1, Nombre = "Fútbol" },
                new Deportes { Deporte_ID = 2, Nombre = "Básquet" },
                new Deportes { Deporte_ID = 3, Nombre = "Vóley" },
                new Deportes { Deporte_ID = 4, Nombre = "Tenis" }
            );

            // Canchas
            modelBuilder.Entity<Canchas>().HasData(
                new Canchas { Cancha_ID = 1, Deporte_ID = 1 },
                new Canchas { Cancha_ID = 2, Deporte_ID = 2 },
                new Canchas { Cancha_ID = 3, Deporte_ID = 3 },
                new Canchas { Cancha_ID = 4, Deporte_ID = 4 }
            );


            // Proveedores
            modelBuilder.Entity<Proveedores>().HasData(
                new Proveedores { Proveedor_ID = 1, Nombre = "Proveedor1", Direccion = "Buenos aires 510" , Email = "prov1@test.com", Telefono = "1111" },
                new Proveedores { Proveedor_ID = 2, Nombre = "Proveedor2", Direccion = "Buenos aires 511" , Email = "prov2@test.com", Telefono = "2222" },
                new Proveedores { Proveedor_ID = 3, Nombre = "Proveedor3", Direccion = "Buenos aires 512" , Email = "prov3@test.com", Telefono = "3333" },
                new Proveedores { Proveedor_ID = 4, Nombre = "Proveedor4", Direccion = "Buenos aires 513" , Email = "prov4@test.com", Telefono = "4444" }
            );

            // Productos
            modelBuilder.Entity<Productos>().HasData(
                new Productos { Producto_ID = 1,  TipoProducto = "Agua Mineral", Proveedor_ID = 1, Precio = 500 },
                new Productos { Producto_ID = 2,  TipoProducto = "Papas Fritas", Proveedor_ID = 2, Precio = 800 },
                new Productos { Producto_ID = 3,  TipoProducto = "Gaseosa", Proveedor_ID = 3, Precio = 700 },
                new Productos { Producto_ID = 4,  TipoProducto = "Barrita Energética", Proveedor_ID = 4, Precio = 600 }
            );
           

            // Turnos
            modelBuilder.Entity<Turnos>().HasData(
                new Turnos
                {
                    Turno_ID = 1,
                    HoraInicio = new DateTime(2024, 1, 1, 9, 0, 0),
                    HoraFin = new DateTime(2024, 1, 1, 10, 0, 0),
                    Cliente_ID = 1,
                    Admin_ID = 1,
                    Cancha_ID = 1
                },
                new Turnos
                {
                    Turno_ID = 2,
                    HoraInicio = new DateTime(2024, 1, 1, 10, 0, 0),
                    HoraFin = new DateTime(2024, 1, 1, 11, 0, 0),
                    Cliente_ID = 2,
                    Admin_ID = 2,
                    Cancha_ID = 2
                }
            );
            // TurnoProducto
            modelBuilder.Entity<TurnoProducto>().HasData(
                new TurnoProducto { TurnoProducto_ID = 1, Turno_ID = 1, Producto_ID = 1, Cantidad = 2},
                new TurnoProducto { TurnoProducto_ID = 2, Turno_ID = 2, Producto_ID = 2, Cantidad = 1},
                new TurnoProducto { TurnoProducto_ID = 3, Turno_ID = 3, Producto_ID = 3, Cantidad = 3},
                new TurnoProducto { TurnoProducto_ID = 4, Turno_ID = 4, Producto_ID = 4, Cantidad = 2}
            );

         
        }

    }
}