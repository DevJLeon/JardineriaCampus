
# Sistema Administracion Jardinería :D 🌱🤑💵
Sistema de administración de tienda de jardinería que permite llevar el control, registro y seguimiento de los productos vendidos, así como sus clientes, oficinas y empleados.

## Características 🌟

- Registro de usuarios.
- Autenticación con usuario y contraseña.
- Generación y utilización del token.
- CRUD completo para cada entidad.
- Endpoints requeridos

## 1. Generación del token 🔑:

**Endpoint**: `http://localhost:5184/api/usuario/token`

**Método**: `POST`

**Payload**:

`{
    "Nombre": "Admin",
    "Contraseña": "123"
}`

Se obtendrá el token y se concede el acceso a los métodos correspondientes ✅
## 2. Registro de Usuarios 📝:

**Endpoint**: `http://localhost:5184/api/usuario/register`

**Método**: `POST`

**Payload**:

json
`{
    "Usuario": "<usuario>",
    "Contraseña": "<contraseña>",
    "CorreoElectronico": "<correo>"
}`

Este endpoint permite a los usuarios registrarse en el sistema.

Una vez registrado el usuario tendrá que ingresar para recibir un token, este será ingresado al siguiente Endpoint que es el de Refresh Token.

## 3. Refresh Token 🔄:

**Endpoint**: `http://localhost:5184/api/usuario/refresh-token`

**Método**: `POST`

**Payload Token**:

`{
    "Nombre": "<usuario>",
    "Contraseña": "<contraseña>"
}`

Se dejan los mismos datos en el Body y luego se ingresa al "Auth", "Bearer", allí se ingresa el token obtenido en el anterior Endpoint.

**Otros Endpoints Usuario👤**

Obtener Todos los Usuarios: GET `http://localhost:5184/api/usuario`

Obtener Usuario por ID: GET `http://localhost:5184/api/usuario/{id}`

Actualizar Usuario: PUT `http://localhost:5184/api/usuario/{id}`

Eliminar Usuario: DELETE `http://localhost:5184/api/usuario/{id}`

- Datos Usuario:
```
  {
      "id": 1,
      "username": "admin",
      "email": "admin@example.com",
      "password": "AQAAAAIAAYagAAAAEGi0FjrgCdfzlsJOcti7rJQq1k5DB4Cpr6ICusQEnePc6f78PE1IhgVuIzm6rwMeDA"
    }
```
## Uso 🕹
**ENPOINTS DE CADA TABLA🟢**
**CLIENTE:**

http://localhost:5184/api/cliente/consulta1

http://localhost:5184/api/cliente/consulta3
http://localhost:5184/api/cliente/consulta11
http://localhost:5184/api/cliente/consulta12
http://localhost:5184/api/cliente/consulta13
http://localhost:5184/api/cliente/consulta14
http://localhost:5184/api/cliente/consulta15
http://localhost:5184/api/cliente/consulta16
http://localhost:5184/api/cliente/consulta18
http://localhost:5184/api/cliente/consulta19
http://localhost:5184/api/cliente/consulta20
http://localhost:5184/api/cliente/consulta21
http://localhost:5184/api/cliente/consulta27
http://localhost:5184/api/cliente/consulta30
http://localhost:5184/api/cliente/consulta33
http://localhost:5184/api/cliente/consulta34
http://localhost:5184/api/cliente/consulta36
http://localhost:5184/api/cliente/consulta37
http://localhost:5184/api/cliente/consulta45
http://localhost:5184/api/cliente/consulta48
http://localhost:5184/api/cliente/consulta49
http://localhost:5184/api/cliente/consulta51
http://localhost:5184/api/cliente/consulta52
http://localhost:5184/api/cliente/consulta55
http://localhost:5184/api/cliente/consulta56
http://localhost:5184/api/cliente/consulta57
http://localhost:5184/api/cliente/consulta58
http://localhost:5184/api/cliente/consulta59
http://localhost:5184/api/cliente/consulta60

**EMPLEADO**

http://localhost:5184/api/empleado/consulta17


http://localhost:5184/api/empleado/consulta22
http://localhost:5184/api/empleado/consulta23
http://localhost:5184/api/empleado/consulta28
http://localhost:5184/api/empleado/consulta29
http://localhost:5184/api/empleado/consulta35
http://localhost:5184/api/empleado/consulta54
http://localhost:5184/api/empleado/consulta61

**OFICINA**
http://localhost:5184/api/oficina/consulta26/

**PAGO**

http://localhost:5184/api/pago/consulta8
http://localhost:5184/api/pago/consulta9
http://localhost:5184/api/pago/consulta31

**PEDIDO**

http://localhost:5184/api/pedido/consulta2

http://localhost:5184/api/pedido/consulta4

http://localhost:5184/api/pedido/consulta5
http://localhost:5184/api/pedido/consulta6
http://localhost:5184/api/pedido/consulta7
http://localhost:5184/api/pedido/consulta32
http://localhost:5184/api/pedido/consulta38
http://localhost:5184/api/pedido/consulta39
http://localhost:5184/api/pedido/consulta53


**PRODUCTO**

http://localhost:5184/api/producto/consulta10

http://localhost:5184/api/producto/consulta24
http://localhost:5184/api/producto/consulta25
http://localhost:5184/api/producto/consulta40
http://localhost:5184/api/producto/consulta41
http://localhost:5184/api/producto/consulta42
http://localhost:5184/api/producto/consulta43
http://localhost:5184/api/producto/consulta46
http://localhost:5184/api/producto/consulta47
http://localhost:5184/api/producto/consulta50

1. Devuelve un listado con el nombre de los todos los clientes españoles.❓

2. Devuelve un listado con los distintos estados por los que puede pasar un
pedido.❓

3. Devuelve un listado con el código de cliente de aquellos clientes que
realizaron algún pago en 2008. Tenga en cuenta que deberá eliminar
aquellos códigos de cliente que aparezcan repetidos. Resuelva la consulta:
• Utilizando la función YEAR de MySQL.❓
• Utilizando la función DATE_FORMAT de MySQL.
• Sin utilizar ninguna de las funciones anteriores.

4. Devuelve un listado con el código de pedido, código de cliente, fecha
esperada y fecha de entrega de los pedidos que no han sido entregados a
tiempo.❓

5. Devuelve un listado con el código de pedido, código de cliente, fecha
esperada y fecha de entrega de los pedidos cuya fecha de entrega ha sido al
menos dos días antes de la fecha esperada.
• Utilizando la función ADDDATE de MySQL.❓
• Utilizando la función DATEDIFF de MySQL.
• ¿Sería posible resolver esta consulta utilizando el operador de suma + o
resta -?

6. Devuelve un listado de todos los pedidos que fueron rechazados en 2009.❓

7. Devuelve un listado de todos los pedidos que han sido entregados en el
mes de enero de cualquier año.❓

8. Devuelve un listado con todos los pagos que se realizaron en el
año 2008 mediante Paypal. Ordene el resultado de mayor a menor.❓

9. Devuelve un listado con todas las formas de pago que aparecen en la
tabla pago. Tenga en cuenta que no deben aparecer formas de pago
repetidas.❓

10. Devuelve un listado con todos los productos que pertenecen a la
gama Ornamentales y que tienen más de 100 unidades en stock. El listado
deberá estar ordenado por su precio de venta, mostrando en primer lugar
los de mayor precio.❓

11. Devuelve un listado con todos los clientes que sean de la ciudad de Madrid y
cuyo representante de ventas tenga el código de empleado 11 o 30.❓


// not this1.4.5 Consultas multitabla (Composición interna)
Resuelva todas las consultas utilizando la sintaxis de SQL1 y SQL2. Las consultas con
sintaxis de SQL2 se deben resolver con INNER JOIN y NATURAL JOIN.


12. Obtén un listado con el nombre de cada cliente y el nombre y apellido de su
representante de ventas.-❓

13. Muestra el nombre de los clientes que hayan realizado pagos junto con el
nombre de sus representantes de ventas.❓

14. Muestra el nombre de los clientes que no hayan realizado pagos junto con
el nombre de sus representantes de ventas.❓

15. Devuelve el nombre de los clientes que han hecho pagos y el nombre de sus
representantes junto con la ciudad de la oficina a la que pertenece el
representante.❓

16. Devuelve el nombre de los clientes que no hayan hecho pagos y el nombre
de sus representantes junto con la ciudad de la oficina a la que pertenece el
representante.❓

17. Devuelve un listado que muestre el nombre de cada empleados, el nombre
de su jefe y el nombre del jefe de sus jefe. ❓🔥🔥🔥🔥🔥🔥🔥🔥🔥🔥🔥🔥🔥🔥🔥🔥🔥🔥

18. Devuelve el nombre de los clientes a los que no se les ha entregado a
tiempo un pedido.❓

19. Devuelve un listado de las diferentes gamas de producto que ha comprado
cada cliente.❓🔥🔥🔥🔥


//not this 1.4.6 Consultas multitabla (Composición externa)
Resuelva todas las consultas utilizando las cláusulas LEFT JOIN, RIGHT JOIN, NATURAL
LEFT JOIN y NATURAL RIGHT JOIN.

20. Devuelve un listado que muestre solamente los clientes que no han
realizado ningún pago.❓

21. Devuelve un listado que muestre los clientes que no han realizado ningún
pago y los que no han realizado ningún pedido.❓

22. Devuelve un listado que muestre solamente los empleados que no tienen un
cliente asociado junto con los datos de la oficina donde trabajan.❓

23. Devuelve un listado que muestre los empleados que no tienen una oficina
asociada y los que no tienen un cliente asociado.❓

24. Devuelve un listado de los productos que nunca han aparecido en un
pedido.❓

25. Devuelve un listado de los productos que nunca han aparecido en un
pedido. El resultado debe mostrar el nombre, la descripción y la imagen del
producto.❓

26. Devuelve las oficinas donde no trabajan ninguno de los empleados que
hayan sido los representantes de ventas de algún cliente que haya realizado
la compra de algún producto de la gama Frutales.❓❓❓🔥🔥🔥(check later!!!!!!!!!!!!!!!!)🔥🔥🔥

27. Devuelve un listado con los clientes que han realizado algún pedido pero no
han realizado ningún pago.❓❓

28. Devuelve un listado con los datos de los empleados que no tienen clientes
asociados y el nombre de su jefe asociado.❓❓

// not this1.4.7 Consultas resumen

29. ¿Cuántos empleados hay en la compañía?❓

30. ¿Cuántos clientes tiene cada país?❓

31. ¿Cuál fue el pago medio en 2009?❓

32. ¿Cuántos pedidos hay en cada estado? Ordena el resultado de forma
descendente por el número de pedidos.❓

33. ¿Cuántos clientes existen con domicilio en la ciudad de Madrid?❓

34. ¿Calcula cuántos clientes tiene cada una de las ciudades que empiezan
por M?❓

35. Devuelve el nombre de los representantes de ventas y el número de clientes
al que atiende cada uno.❓

36. Calcula el número de clientes que no tiene asignado representante de
ventas.❓

37. Calcula la fecha del primer y último pago realizado por cada uno de los
clientes. El listado deberá mostrar el nombre y los apellidos de cada cliente.❓

38. Calcula el número de productos diferentes que hay en cada uno de los
pedidos. ****

39. Calcula la suma de la cantidad total de todos los productos que aparecen en
cada uno de los pedidos. ***

40. Devuelve un listado de los 20 productos más vendidos y el número total de
unidades que se han vendido de cada uno. El listado deberá estar ordenado
por el número total de unidades vendidas. ***

41. La misma información que en la pregunta anterior, pero agrupada por
código de producto.****

42. La misma información que en la pregunta anterior, pero agrupada por
código de producto filtrada por los códigos que empiecen por OR. ****

43. Lista las ventas totales de los productos que hayan facturado más de 3000
euros. Se mostrará el nombre, unidades vendidas, total facturado y total
facturado con impuestos (21% IVA). ****

44. Muestre la suma total de todos los pagos que se realizaron para cada uno
de los años que aparecen en la tabla pagos. ********?????????

// not this 1.4.8 Subconsultas

// not this 1.4.8.1 Con operadores básicos de comparación

45. Devuelve el nombre del cliente con mayor límite de crédito. ****

46. Devuelve el nombre del producto que tenga el precio de venta más caro. ****

47. Devuelve el nombre del producto del que se han vendido más unidades.
(Tenga en cuenta que tendrá que calcular cuál es el número total de
unidades que se han vendido de cada producto a partir de los datos de la
tabla detalle_pedido)  ****

48. Los clientes cuyo límite de crédito sea mayor que los pagos que haya
realizado. (Sin utilizar INNER JOIN). ****
1.4.8.2 Subconsultas con ALL y ANY

49. Devuelve el nombre del cliente con mayor límite de crédito. ****

50. Devuelve el nombre del producto que tenga el precio de venta más caro. ****

// not this 1.4.8.3 Subconsultas con IN y NOT IN

51. Devuelve un listado que muestre solamente los clientes que no han
realizado ningún pago.****

52. Devuelve un listado que muestre solamente los clientes que sí han realizado
algún pago.****

53. Devuelve un listado de los productos que nunca han aparecido en un
pedido.****

54. Devuelve el nombre, apellidos, puesto y teléfono de la oficina de aquellos
empleados que no sean representante de ventas de ningún cliente.****
1.4.8.4 Subconsultas con EXISTS y NOT EXISTS

55. Devuelve un listado que muestre solamente los clientes que no han
realizado ningún pago. ****

56. Devuelve un listado que muestre solamente los clientes que sí han realizado
algún pago. ****
// not this 1.4.8.5 Subconsultas correlacionadas
// not this 1.4.9 Consultas variadas

57. Devuelve el listado de clientes indicando el nombre del cliente y cuántos
pedidos ha realizado. Tenga en cuenta que pueden existir clientes que no
han realizado ningún pedido. ****

58. Devuelve el nombre de los clientes que hayan hecho pedidos en 2008
ordenados alfabéticamente de menor a mayor. ****

59. Devuelve el nombre del cliente, el nombre y primer apellido de su
representante de ventas y el número de teléfono de la oficina del
representante de ventas, de aquellos clientes que no hayan realizado ningún
pago. ****

60. Devuelve el listado de clientes donde aparezca el nombre del cliente, el
nombre y primer apellido de su representante de ventas y la ciudad donde
está su oficina. ****

61. Devuelve el nombre, apellidos, puesto y teléfono de la oficina de aquellos
empleados que no sean representante de ventas de ningún cliente.