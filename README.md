 Sistema de Pedido de Pizzas 

Sistema distribuido para la gestión de pedidos de pizzas.


Esta versión del sistema introduce dos nuevos módulos clave:

 1.  Módulo: Roles de Usuario
Propósito: Gestionar los permisos y la autenticación de los diferentes usuarios del sistema  Administrador, Cliente.

 Funciones principales:
  AsignarRol(idUsuario, rol)`: Establece el nivel de acceso para un usuario.
  VerificarPermiso(idUsuario, accion): Confirma si un usuario tiene la autoridad para realizar una acción específica.
     

 2.  Módulo: Gestión de Pizzas
Propósito: Permite a los usuarios autorizados (ej: Administradores) crear, modificar o eliminar los tipos de pizzas y sus ingredientes disponibles en el menú.

Funciones principales:
    Crear NuevaPizza: Agrega una nueva pizza al menú.
    Editar Pizza: Modifica el nombre, precio o ingredientes de una pizz
