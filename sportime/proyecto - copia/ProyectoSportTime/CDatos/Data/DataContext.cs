using Microsoft.AspNetCore.Mvc;
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

            //Usuarios
            modelBuilder.Entity<Usuarios>().HasData(
            new Usuarios { Usuario_ID = 1, Nombre = "Usuario1", Email = "usuario1@test.com", NumeroTelefono = "3493112233", Contraseña = "1111", Rol = "Cliente" },
            new Usuarios { Usuario_ID = 2, Nombre = "Usuario2", Email = "usuario2@test.com", NumeroTelefono = "3493112244", Contraseña = "2222", Rol = "Administrador" }
            );

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
                    Estado = "Confirmado",
                    Usuario_ID = 1,                    
                    Cancha_ID = 1
                },
                new Turnos
                {
                    Turno_ID = 2,
                    HoraInicio = new DateTime(2024, 1, 1, 10, 0, 0),
                    HoraFin = new DateTime(2024, 1, 1, 11, 0, 0),
                    Estado = "Confirmado",
                    Usuario_ID = 2,
                    Cancha_ID = 2
                }
            );
            // TurnoProducto
            modelBuilder.Entity<TurnoProducto>().HasData(
                new TurnoProducto { TurnoProducto_ID = 1, Turno_ID = 1, Producto_ID = 1, Cantidad = 2},
                new TurnoProducto { TurnoProducto_ID = 2, Turno_ID = 2, Producto_ID = 2, Cantidad = 1}
              
            );

         
        }

    }
}