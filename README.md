A continuación, explico qué controladores desarrollé, las razones detrás de esta decisión arquitectónica, y los pros y contras de este diseño.

1. ¿Qué controladores desarrollé y para qué sirve cada uno?
AccountController (Autenticación y Seguridad): Lo mantuve de forma totalmente independiente. Centraliza de manera exclusiva el inicio de sesión (Login), el cierre de sesión (Logout) y la verificación de credenciales contra la tabla LOGIN de la base de datos, asegurando que las contraseñas viajen cifradas mediante BCrypt.
HomeController (Portal General e Historial del Alumno): Es el punto de entrada de la aplicación. Actúa como redireccionador inteligente según el rol del usuario logueado. Además, absorbe la vista de los estudiantes (el "Portal del Alumno"), permitiéndoles consultar su porcentaje de asistencia e historial de forma ágil y en modo de solo lectura.
ProfesorController (Flujo del Docente y Carga de Asistencias): Es el núcleo operativo del aula. Permite al docente seleccionar la materia, cohorte y carrera, y despliega la planilla digital de alumnos. Implementa una lógica inteligente que detecta si la asistencia del día ya fue tomada (permitiendo una edición/UPDATE rápida) o si es nueva (realizando un INSERT masivo en la tabla ASISTENCIAS).
AdminController (Panel de Control y CRUDs de Gestión): Unifica todas las funciones con privilegios de administrador o preceptor. Alberga el Dashboard con las métricas generales del instituto, el ABM (CRUD) completo de Usuarios (alumnos/docentes) controlando duplicados de DNI y calculando claves primarias de forma incremental, y la lógica de justificación de inasistencias médicas.
2. ¿Por qué elegí este diseño? (Justificación Arquitectónica)
La base de datos original (InstitutoNew.sql) cuenta con un esquema de 10 tablas relacionadas. Inicialmente, se podría pensar en crear un controlador por cada recurso (uno para materias, uno para carreras, uno para usuarios, etc., resultando en 8 o 9 controladores).

Sin embargo, opté por una arquitectura basada en Roles de Usuario por las siguientes razones:

Simplicidad del flujo: El tamaño actual del proyecto no justifica la complejidad de administrar 9 controladores independientes con sus respectivas carpetas de vistas.
Facilidad de navegación: Agrupar las funciones por "Quién hace qué" (Preceptor vs. Docente vs. Alumno) hace que el enrutamiento de ASP.NET Core MVC sea extremadamente limpio e intuitivo.
Seguridad limpia: Me permite aplicar políticas de autorización ([Authorize(Roles = "Admin")] o [Authorize(Roles = "Docente")]) directamente a nivel de clase completa, blindando el controlador entero con una sola línea de código.
3. Ventajas y Desventajas del Diseño
Ventajas (Pros):
Orden y Limpieza en el Repositorio: Al consolidar la lógica, evito saturar el proyecto con decenas de archivos. Mantenemos el código ordenado dentro de los 4 controladores que ya venían en la estructura inicial.
Fácil control de seguridad corporativa: Es muy sencillo asegurar que un profesor no acceda a pantallas de administración o viceversa, ya que la barrera de seguridad está definida por archivo físico.
Desarrollo en paralelo ágil: Al estar trabajando en GitHub en equipo (rama Grupo-2), un desarrollador puede enfocarse completamente en las funcionalidades del ProfesorController mientras otro trabaja de forma aislada en el AdminController, reduciendo a cero la posibilidad de tener conflictos de código (merge conflicts).
Rendimiento optimizado en lote: La carga masiva de asistencias se realiza en una sola transacción transaccional hacia SQL Server, evitando múltiples llamadas innecesarias al servidor.
Desventajas (Contras):
Riesgo de "Controladores Gordos" (Fat Controllers): Al unificar la gestión de usuarios, métricas y justificaciones en el AdminController, este archivo de código tiende a volverse muy extenso.
Cómo lo solucioné/mitigué: Comenté minuciosamente cada sección del código mediante regiones (#region) y mantuve los métodos enfocados únicamente en la respuesta HTTP, derivando la validación del modelo al DbContext para evitar código espagueti.
Acoplamiento de datos (Falta de relación Alumnos ➔ Carreras): Como la base de datos no tiene una tabla que asocie qué alumno cursa qué carrera, me vi en la necesidad de listar en el ProfesorController a todos los usuarios con rol de Alumno.
Mejora a futuro: Si en el futuro decidimos agregar la tabla intermedia faltante en la base de datos, el ProfesorController se beneficiará inmediatamente permitiendo filtrar la lista de alumnos sin necesidad de cambiar la estructura de controladores, solo refinando la consulta LINQ.
