# Guía para trabajar en Arkanoid

Repositorio del grupo: [arsenii-g/Arkanoid](https://github.com/arsenii-g/Arkanoid).

Versión del proyecto: **Unity 6.4, exactamente 6000.4.12f1**. El número también está en `ProjectSettings/ProjectVersion.txt`.

## 1. Qué necesita cada uno

- Unity Hub y Unity Editor **6000.4.12f1**. Si esa versión no aparece en Hub, búscala en el [archivo oficial de Unity](https://unity.com/releases/editor/archive) y usa su enlace a Unity Hub.
- Internet en la primera apertura para que Unity descargue los paquetes del proyecto.
- Una cuenta propia de GitHub y aceptar la invitación de colaborador para subir cambios al repositorio del grupo.

El repositorio es público: podéis descargarlo sin aceptar la invitación. Para subir cambios, entrad en GitHub con la cuenta invitada, revisad el correo o las notificaciones y pulsad **Accept invitation**. No compartáis una misma cuenta entre los tres.

**GitHub Desktop es opcional.** Se puede abrir y modificar el juego usando solamente Unity y el navegador. Las secciones 2 a 5 explican esa opción. La sección 6 explica cómo usar Git desde la terminal.

## 2. Descargar y abrir sin instalar Git ni GitHub Desktop

1. Abre [el repositorio](https://github.com/arsenii-g/Arkanoid) en el navegador.
2. Comprueba que está seleccionada la rama **main**.
3. Pulsa **Code → Download ZIP**.
4. Descomprime el ZIP en una carpeta local, por ejemplo `Documentos/Arkanoid-main`. No abras el proyecto dentro del ZIP.
5. Comprueba que dentro están las carpetas **Assets**, **Packages** y **ProjectSettings**.
6. Abre **Unity Hub → Projects → Add → Add project from disk**. El nombre del botón puede variar según la versión de Hub.
7. Selecciona la carpeta que contiene esas tres carpetas. No selecciones `Assets` ni crees un proyecto vacío nuevo.
8. Abre el proyecto con **6000.4.12f1**. Si Hub pide instalar el editor, instala esa versión.
9. Espera a que termine de importar y compilar. La primera apertura puede tardar más; Unity crea `Library` en tu ordenador.

Una descarga ZIP es una copia de los archivos: **no tiene conexión de Git para hacer `pull` o `push`**. Para recibir cambios con este método, descarga un ZIP nuevo y descomprímelo en otra carpeta. Conserva tu copia anterior si contiene cambios tuyos pendientes.

## 3. Poner la resolución y jugar

1. En la ventana **Project**, entra en `Assets/Scenes`.
2. Abre **Menu** con doble clic.
3. En la pestaña **Game**, abre el desplegable de resolución y selecciona **1920 × 1080**.
4. Si no aparece, pulsa **+**, elige **Fixed Resolution**, escribe anchura **1920** y altura **1080**, y guarda el tamaño. Puedes llamarlo `Full HD`.
5. Ajusta **Scale** para ver la imagen completa en tu monitor. Ese zoom de la ventana no cambia la resolución del juego.
6. Pulsa el botón triangular **Play** de Unity y después **PLAY** en el menú del juego.

Controles:

| Acción | Tecla |
| --- | --- |
| Mover la raqueta | A/D o flechas izquierda/derecha |
| Lanzar la bola | Espacio |

Hay **un nivel**, con **33 ladrillos individuales**, repartidos en tres filas. Cada ladrillo da **100 puntos**. Si la bola cae por debajo de la raqueta, aparece la derrota; al destruir todos los ladrillos, la victoria. Ambas pantallas permiten volver a jugar o regresar al menú.

En las pantallas de resultado, **RETRY** reinicia el nivel y **MENU** vuelve al inicio. Los botones usan **Sprite Swap** para mostrar su imagen pulsada mientras mantienes el clic. Las imágenes están en `Assets/Sprites/BotonesResultado`. La [explicación de cambios](CambiosYFuncionamiento.md) detalla su configuración y los scripts del juego.

Haz clic dentro de **Game** si el teclado no responde. Para terminar la prueba, vuelve a pulsar el botón Play de Unity. Los cambios que hagas en los objetos durante Play normalmente se pierden al salir: edita el nivel con Play detenido.

Los Canvas ya tienen **Scale With Screen Size**, referencia **1920 × 1080** y **Match 0,5**. La elección de tamaño en la pestaña Game es local a cada editor: cada compañero debe seleccionarla en su ordenador.

## 4. Dónde hacer cambios

| Qué quieres cambiar | Dónde está |
| --- | --- |
| Menú y botones iniciales | `Assets/Scenes/Menu.unity` |
| Diseño del nivel | `Assets/Scenes/Nivel 1.unity` |
| Pantallas de resultado | `Assets/Scenes/Victory.unity` y `Defeat.unity` |
| Movimiento de la raqueta | `Assets/Scripts/PaddleMovement.cs` y el objeto `Paddle` |
| Lanzamiento, velocidad y rebotes de la bola | `Assets/Scripts/Ball.cs` y el objeto `Bola` |
| Resistencia y puntos por ladrillo | `Assets/Scripts/Brick.cs` y el prefab `Assets/Prefabs/Brick.prefab` |
| Contador, puntuación y fin de partida | `Assets/Scripts/GameManager.cs` |
| Volver a jugar o al menú | `Assets/Scripts/ResultadoPartida.cs` |
| Efectos de sonido | `Assets/Audio` |

Ejemplo: para mover un ladrillo, abre **Nivel 1**, despliega **Ladrillos → Fila roja/azul/naranja** en Hierarchy y selecciona un ladrillo. Cambia su **Transform** con Play detenido y guarda la escena con **Ctrl+S** en Windows o **Cmd+S** en Mac.

Para añadir otro ladrillo, duplica uno existente desde Unity y colócalo. Mantén su componente **Brick**, su **BoxCollider2D**, el tag **Brick** y la capa **Escenario**. El contador recoge automáticamente los ladrillos activos al empezar la partida.

El fondo es el objeto **Fondo Estrellas**. Mantén una única copia: las copias superpuestas con el mismo orden de dibujo provocaban saltos de las estrellas al destruir ladrillos.

Si mueves o renombras assets, hazlo desde la ventana **Project** de Unity. Cada asset tiene un archivo **.meta** con su identificador; ese identificador mantiene enlazados los sprites, scripts y escenas. Comparte los `.meta` junto con los assets nuevos y conserva los existentes.

Antes de compartir un cambio:

1. Guarda las escenas y los scripts.
2. Espera a que termine de compilar y revisa la **Console**. Corrige los errores rojos.
3. Prueba lo que hayas modificado: menú, lanzamiento, rebotes, puntos o botones, según corresponda.
4. Detén Play y guarda otra vez si has hecho ajustes fuera de Play.

## 5. Subir cambios desde el navegador, sin instalar Git

Este método sirve para cambios pequeños cuando has trabajado sobre un ZIP reciente.

1. Guarda el proyecto y anota qué archivos has cambiado o creado.
2. Abre el repositorio con tu cuenta de colaborador.
3. Entra en la carpeta de destino. Por ejemplo, para un script entra en **Assets/Scripts**; para una escena, en **Assets/Scenes**.
4. Pulsa **Add file → Upload files** y selecciona los archivos correspondientes de tu copia local. Incluye los `.meta` de los assets nuevos.
5. Comprueba las rutas y los nombres: `Ball.cs` debe seguir en `Assets/Scripts`, y la escena en `Assets/Scenes`.
6. Escribe un mensaje que describa el cambio, por ejemplo `Ajustar velocidad de la bola`.
7. Elige crear **una rama nueva** para el cambio, por ejemplo `victor/velocidad-bola`, y confirma la subida.
8. Si faltan archivos en otras carpetas, selecciona esa misma rama en GitHub y súbelos en sus carpetas antes de abrir la revisión.
9. Abre una **pull request** hacia `main`: **base: main**, **compare: tu rama**. Describe qué cambiaste y cómo lo probaste.
10. Otro compañero revisa la pestaña **Files changed** y, si está correcto, une la pull request con **Merge**.

Sube los archivos del proyecto que has modificado, no el ZIP completo ni `Library`, `Temp`, `Logs` o `UserSettings`. La subida web tiene un límite de **25 MiB por archivo**. Para cambios grandes, renombrados o eliminaciones, usa Git siguiendo la sección 6.

Si otro compañero ha cambiado el mismo archivo desde que descargaste el ZIP, descarga una copia reciente y aplica tu cambio sobre ella antes de subirlo. Un archivo antiguo completo podría reemplazar su trabajo.

## 6. Colaborar con Git desde la terminal

Esta opción permite actualizar la misma carpeta y compartir cambios de forma habitual. **Git es una herramienta distinta de GitHub Desktop**: puedes instalar Git sin instalar la aplicación de GitHub.

### 6.1. Preparar una copia clonada, una sola vez

Instala Git desde su [sitio oficial](https://git-scm.com/install/). En Windows puedes usar **Git Bash**; en Mac, **Terminal**. Comprueba la instalación:

```bash
git --version
```

Abre la terminal en la carpeta donde quieras guardar el proyecto y ejecuta:

```bash
git clone https://github.com/arsenii-g/Arkanoid.git
cd Arkanoid
git config user.name "Tu nombre"
git config user.email "Tu correo de GitHub"
```

Sustituye el nombre y correo del ejemplo por los tuyos. Estos dos ajustes se aplican a esta copia del repositorio. Puedes usar el correo privado `noreply` que GitHub muestra en los ajustes de tu cuenta.

Añade esta carpeta **Arkanoid** a Unity Hub como en la sección 2. Si antes usabas un ZIP, conserva esa carpeta aparte mientras tengas cambios pendientes y trabaja desde la nueva carpeta clonada.

En el primer `push`, completa el inicio de sesión que abra el gestor de credenciales de Git. Si la terminal pide una contraseña para GitHub, se utiliza un token de acceso, no la contraseña de la web; consulta la [guía oficial de autenticación](https://docs.github.com/en/authentication/keeping-your-account-and-data-secure/about-authentication-to-github).

### 6.2. Antes de empezar una tarea nueva

Guarda y cierra Unity antes de actualizar archivos del proyecto. Desde la carpeta `Arkanoid`:

```bash
git status
```

Si indica cambios tuyos pendientes, guárdalos en un commit de tu rama antes de continuar. Cuando el estado esté limpio:

```bash
git switch main
git pull --ff-only origin main
git switch -c victor/ajuste-bola
```

Usa un nombre nuevo para cada tarea, por ejemplo `bkg/menu` o `victor/ajuste-bola`. Si retomas una tarea ya empezada, cambia a su rama con `git switch nombre-de-la-rama`; no la crees otra vez.

Después abre Unity y haz los cambios.

### 6.3. Guardar y subir tu trabajo

Guarda, prueba y cierra Unity. Revisa los archivos y los cambios:

```bash
git status
git diff
git add Assets Packages ProjectSettings README.md Documentacion
git diff --cached --stat
git commit -m "Ajustar velocidad de la bola"
git push -u origin victor/ajuste-bola
```

Usa el nombre real de tu rama en el último comando. `add` prepara los archivos; `commit` guarda una versión en tu ordenador; `push` la sube a GitHub.

Entra en GitHub y pulsa **Compare & pull request**, o **Pull requests → New pull request**. Elige tu rama y `main` como destino. Indica qué cambiaste y cómo lo probaste. Otro compañero revisa y une el cambio.

Mientras esa rama siga abierta, puedes hacer más commits y `git push`: se añaden a la misma pull request. No pulses Merge hasta que todos los archivos necesarios estén subidos y revisados.

### 6.4. Recibir el trabajo que ya está unido a main

Con Unity cerrado y sin cambios locales pendientes:

```bash
git switch main
git pull --ff-only origin main
```

Abre de nuevo Unity. Importará los assets que hayan cambiado.

**¿Tengo que hacer pull cada vez que abro Unity, aunque solo haya trabajado yo?** Tus cambios guardados ya están en ese ordenador y no necesitan descargarse de nuevo. Haz `pull` antes de una tarea nueva para recoger cambios que hayan subido los otros, o cuando uses otro ordenador. `pull` descarga; no sube tu trabajo.

## 7. Organizaros los tres

- Decid quién está editando **Nivel 1** y quién está editando el menú. Evitad editar la misma escena o prefab simultáneamente.
- Si dos personas necesitan tocar el nivel, terminad y unid primero un cambio; después, la segunda persona actualiza su copia y empieza.
- Haced ramas y commits por tareas pequeñas, con mensajes claros.
- Revisad las pull requests de otro compañero antes de unirlas.
- Compartid `Assets`, `Packages`, `ProjectSettings`, `Documentacion` y el README. Las carpetas generadas por Unity ya están excluidas en `.gitignore`.
- Todos debéis mantener **6000.4.12f1**. Un cambio de versión se acuerda con el grupo antes de abrir y guardar el proyecto con otro editor.

## 8. Problemas frecuentes

| Problema | Qué hacer |
| --- | --- |
| Hub no reconoce el proyecto | Selecciona la carpeta que contiene `Assets`, `Packages` y `ProjectSettings`; descomprime primero el ZIP. |
| No aparece Unity 6000.4.12f1 | Instálalo desde el archivo oficial de Unity y vuelve a abrir con esa versión. |
| El teclado no mueve la raqueta | Entra en Play y haz clic dentro de Game. |
| El menú parece cortado | Selecciona la resolución fija 1920×1080 y reduce el zoom Scale si no cabe. |
| Una escena tiene cambios pero no llegan al equipo | Guárdala fuera de Play y comparte su archivo `.unity`. |
| Falta un sprite o un script al descargar | Comprueba que se subió el asset junto con su `.meta`, conservando su ruta y su identificador. |
| `not a git repository` | Estás en una carpeta incorrecta o en un ZIP. Para usar Git, trabaja en la carpeta creada por `git clone`. |
| `Permission denied` o HTTP 403 al subir | Acepta la invitación y comprueba que estás autenticado con la cuenta de colaborador. |
| `Your local changes would be overwritten` | Guarda tu trabajo en tu rama con un commit antes de cambiar de rama o actualizar. |
| `Not possible to fast-forward` | Tu rama y GitHub tienen commits distintos. Conserva tu rama y revisad cómo integrar ambos trabajos. |
| Hay un conflicto en una escena | Coordinad qué cambios conservar. Si ocurre durante un merge, `git merge --abort` permite cancelar ese merge para revisarlo después. |

Si hay cambios pendientes o un conflicto, conservad vuestra copia; evitad usar `reset --hard` o forzar un `push` como solución rápida.

## Referencias

- [Descargar el ZIP de un repositorio — GitHub](https://docs.github.com/en/repositories/working-with-files/using-files/downloading-source-code-archives).
- [Subir archivos desde la web — GitHub](https://docs.github.com/en/repositories/working-with-files/managing-files/adding-a-file-to-a-repository).
- [Proyectos en Unity Hub — Unity](https://docs.unity.com/en-us/hub/projects).
- [Archivos .meta — Unity](https://docs.unity3d.com/Manual/AssetMetadata.html).
- [Actualizar una copia con git pull — Git](https://git-scm.com/docs/git-pull).

Para entender los scripts del juego, consulta [Funcionamiento.md](Funcionamiento.md).
