Paso a paso para subir ficheros con .NET 

Link a videos Youtube =<a href="https://www.youtube.com/playlist?list=PLLbF6JGZ5FrHP-oFAjIysoUlah9lzWiLe">Link a Youtube</a>


1.Creación de un proyecto de tipo Web Api.En este caso el proyecto se llama WebApiFicheros y contendrá los dos endpoints para subir y bajar archivos 
<img src="screenShots\Captura_webApi.PNG">

2..Se procede a eliminar los archivos de controladores de  WeatherForcast que se crean por defecto en la creación de un proyecto.

3.Crearemos una carpeta de servicios donde se creará una interfaz de FicheroService y la clase que implementa los métodos para subir y bajar ficheros a través de un web Api.
<img src="screenShots\CapturaCarpetaService.PNG">
.
4.En la interfaz se declaran 4 métodos, dos para subir archivos (archivo normal, archivo en base64) y dos para bajar documentos.
<img src="screenShots\Ifichero.PNG">

5. En la clase de Fichero service es necesario realizar la inyección de la dependencia IWebHostEnviroment en el constructor de la clase, esta provee información del entorno de alojamiento web y de la aplicación que se está ejecutando.

<img src="screenShots\Fichero.PNG">
Luego de crear el constructor se generan las funciones de respaldo. A continuación se crea el método para subir un documento a partir de un fichero que se pasa como parámetro de la función.
El tipo IFormFile representa un archivo enviado por un método Http Request.
Se crea la ruta de destino donde se almacenarán los archivo  subidos, para este ejemplo se almacenarán en la carpeta Ficheros Subidos que se encuentra en la raíz del proyecto.Luego se completa el nombre de la ruta agregando al final de esta el nombre del archivo y atravez del metodo combine de la clase Path , se genera la ruta final de destino del fichero.Posteriormente se crea un archivo que apunta al directorio de destino y se copia el contenido del fichero antiguo hacia el nuevo.

<img src="screenShots\SubirDocumento.PNG">

El siguiente método difiere del anterior en que se pasa como argumento de la función el nombre del fichero y el contenido del mismo en forma de string codificado en binario o en base64. La forma de subir el fichero es bastante similar al método anterior, pero ahora se 
convierte el string a un arreglo de bytes representativo de 8 bits que tiene el contenido del documento.Finalmente con el método writeAllBytes se escribe en un archivo con la ruta especificada el contenido que representa el arreglo de bytes.

<img src="screenShots\SubirBase64.PNG">

6. Se procede a crear un controlador que hereda de ControllerBase, llamado FicheroController, y se crean los endpoints de tipo Post para subir los archivos. Para ello necesitamos inyectar el servicio de ficheroService que contiene la funcionalidad requerida.Se crea una variable privada de solo lectura ficheroService que contendrá  la instancia de la clase fichero Service, que se le pasara a través del constructor de la clase.

<img src="screenShots\Controller.PNG">

Es necesario agregar al contenedor de dependencias, la IFicheroService para que el programa reconozca esta interfaz con su respectiva implementación como una dependencia del programa. Para ello se crea una carpeta de Extensions; y dentro de esta carpeta un archivo llamado AplicationServiceExtensions en donde se configuraran las extensiones necesarias de la aplicacion.Se crea un metodo de extension  estático llamado AddAplicationServices en donde se configura el scope de la dependencia.

<img src="screenShots\extensions.PNG">


Es necesario llamar a la función de AddAplicationServices en el archivo program.cs de nuestro Web Api de la siguiente manera.
<img src="screenShots\program.PNG">

Los endpoints deben llamar la función del ficheroService y se encapsulan dentro de un try catch para dar al cliente una respuesta determinada dependiendo del éxito de la carga del fichero.

<img src="screenShots\postSubir.PNG">

Bajar Documentos 

Dentro de la clase 'FicheroService', existen dos métodos para descargar un archivo. El primero es el método 'BajarDocumento', que requiere del servicio 'WebHostEnvironment' para acceder a las ubicaciones dentro de la aplicación. A través de este servicio, se obtiene la ubicación completa del directorio donde se encuentra el archivo. Luego, utilizando el método 'ReadAllBytes', el archivo se lee como un arreglo de bytes y se devuelve como resultado.
Posteriormente, en el controlador, se utiliza el método 'File', que pertenece a la clase 'ControllerBase', para retornar un nuevo archivo que contiene el contenido del arreglo de bytes.

<img src="screenShots\BajarDoc.PNG">



También se puede bajar un archivo en forma de un string codificado en base64 que representa el contenido del archivo a través del siguiente método.Luego en el controlador se llama a la función y se devuelve dentro de un Ok, que es un estado 200. 
<img src="screenShots\bajarDocBase64.PNG">
