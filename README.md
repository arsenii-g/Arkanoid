# Arkanoid

Proyecto de clase de un juego 2D hecho con **Unity 6.4 (6000.4.12f1)**.

**Para el equipo:** [Guía detallada para abrir, modificar y compartir el proyecto](Documentacion/GuiaCompaneros.md).

Incluye una opción con ZIP y navegador, sin instalar GitHub Desktop ni Git, y otra con Git para colaborar desde una copia clonada.

## Abrir el proyecto

1. Clona este repositorio o descarga **Code → Download ZIP** y descomprímelo.
2. En Unity Hub, elige **Add project from disk** y selecciona la carpeta clonada.
3. Ábrelo con la versión de Unity indicada arriba. Unity descargará los paquetes y regenerará los archivos locales necesarios.

Las carpetas `Assets`, `Packages` y `ProjectSettings`, incluidos los archivos `.meta`, se comparten en Git. Las carpetas `Library`, `Temp`, `Logs` y `UserSettings` se generan en cada ordenador y se ignoran.

## Resolución y controles

En la pestaña **Game**, selecciona **1920 × 1080** en el desplegable de resolución. Si no aparece, pulsa **+**, elige **Fixed Resolution** y escribe 1920 y 1080. Usa ese tamaño para revisar las escenas. Los Canvas usan **Scale With Screen Size**, referencia 1920 × 1080 y Match 0,5.

Abre **Assets/Scenes/Menu.unity**, entra en Play y pulsa el botón **PLAY**. Mueve la raqueta con **A/D** o las **flechas** y lanza la bola con **Espacio**. Si se cuela por debajo de la raqueta, termina la partida. Al destruir los 33 ladrillos del nivel, se muestra la victoria. Las pantallas de resultado permiten repetir o volver al menú.

El nivel contiene 33 ladrillos independientes, repartidos en tres filas de 11. Cada ladrillo se destruye por separado y vale 100 puntos; completar el nivel da 3300 puntos.

Para importar el paquete en otro proyecto 2D Universal con Unity 6.4, añade los tags **Brick** y **Ball**, pon **Escenario** en la capa 8 y **Pelota** en la 9, activa **Both** en Active Input Handling y añade al Build las escenas **Menu**, **Nivel 1**, **Victory** y **Defeat**, en ese orden.

## Trabajar entre los tres

1. Si tienes una copia clonada, guarda y cierra Unity, comprueba `git status` y actualiza `main` con `git pull --ff-only origin main` cuando no tengas trabajo pendiente. Si usas ZIP, descarga una copia nueva en otra carpeta.
2. Crea una rama para cada cambio: `git switch -c nombre-del-cambio`.
3. Guarda la escena y los assets en Unity, y sube tanto los archivos modificados como sus `.meta`.
4. Abre una pull request en GitHub para que otro compañero revise el cambio antes de unirlo a `main`.

Coordinaos antes de editar simultáneamente la misma escena o prefab: los conflictos en esos archivos pueden requerir una revisión manual en Unity.

Consulta la [guía del equipo](Documentacion/GuiaCompaneros.md) para los comandos completos, cómo subir desde el navegador y qué hacer si aparecen errores. La [explicación del código](Documentacion/Funcionamiento.md) describe la bola, los ladrillos y la puntuación.

El [detalle de cambios y funcionamiento](Documentacion/CambiosYFuncionamiento.md) explica cada script, la revisión con los apuntes y cómo configurar los botones **RETRY** y **MENU** con **Sprite Swap**, sin scripts de animación.
