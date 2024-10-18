-- Primero ejecute la base de datos, ejecute solo la primera linea, despues todas las demas juntas.

Create database GestionInventario;

Use GestionInventario;

Create table Categorias(
IdCategoria int primary key identity(1,1),
Nombre varchar(100) not null,
Descripcion varchar(120)
);

Create table Proveedores(
IdProveedor int primary key identity(1,1),
NombreEmpresa varchar(100) not null,
Contacto varchar(50),
Direccion varchar(150)
);



Create table Productos(
CodigoProducto int primary key identity(1,1),
NombreProduct varchar(100) not null,
IdCategoria int not null,
Precio decimal not null,
Cantidad int default 1,
IdProveedor int not null


Constraint RelacionIdProveedor FOREIGN KEY (IdProveedor)
        References Proveedores(IdProveedor) ON DELETE CASCADE, 
    Constraint RelacionIdCategorias FOREIGN KEY (IdCategoria)
        References Categorias(IdCategoria) ON DELETE CASCADE

);

INSERT INTO Categorias (Nombre, Descripcion)
VALUES 
('Electrónica', 'Dispositivos y aparatos electrónicos'),
('Ropa', 'Vestimenta para todas las edades y géneros'),
('Alimentos', 'Productos de alimentación y bebidas'),
('Juguetes', 'Juguetes y artículos de entretenimiento para niños'),
('Muebles', 'Artículos para el hogar y la oficina');

-- Insertar datos en la tabla Proveedores
INSERT INTO Proveedores (NombreEmpresa, Contacto, Direccion)
VALUES
('TechCorp', 'Juan Pérez', 'Av. Siempre Viva 123, Ciudad A'),
('Fashionista', 'Ana López', 'Calle de la Moda 456, Ciudad B'),
('Foodies', 'Carlos Sánchez', 'Plaza del Sabor 789, Ciudad C'),
('ToysWorld', 'Lucía Martínez', 'Parque de Juegos 101, Ciudad D'),
('FurnitureHouse', 'Roberto Gómez', 'Av. del Hogar 202, Ciudad E');

-- Insertar datos en la tabla Productos
INSERT INTO Productos (NombreProduct, IdCategoria, Precio, Cantidad, IdProveedor)
VALUES
('Smartphone X', 1, 499.99, 50, 1),
('Camiseta Casual', 2, 19.99, 100, 2),
('Chocolate Orgánico', 3, 4.50, 200, 3),
('Muñeca Barbie', 4, 24.99, 150, 4),
('Silla Ergonómica', 5, 129.99, 30, 5),
('Laptop Pro', 1, 999.99, 25, 1),
('Pantalones Vaqueros', 2, 39.99, 80, 2),
('Bebida Energética', 3, 2.99, 300, 3),
('Puzzle 1000 Piezas', 4, 12.99, 100, 4),
('Mesa de Oficina', 5, 199.99, 20, 5);
