# Cambios del Arkanoid y cómo funciona

## 1. Qué se ha cambiado

El proyecto parte del menú, la raqueta, las paredes y la distribución de ladrillos que ya estaban en la escena. La lógica de juego se completó sobre esa base. Hay un solo nivel: **Nivel 1**.

| Archivo o elemento | Trabajo realizado |
| --- | --- |
| `PaddleMovement.cs` | Se conservó el movimiento que ya existía en esta fase: entrada horizontal y límite de posición. |
| `MenuManager.cs` | Se conservó el código para jugar y salir. |
| `Ball.cs` | Se añadió el lanzamiento, movimiento, detección de obstáculos, rebotes y sonidos de la bola. |
| `Brick.cs` | Se amplió el script existente para recibir golpes desde la bola y evitar contar dos veces el mismo ladrillo. |
| `GameManager.cs` | Se amplió el contador y el evento que ya existían con los textos del HUD, puntuación final y parada de la partida. |
| `ResultadoPartida.cs` | Se añadió para mostrar los puntos y atender los botones de repetir y volver al menú. |
| Escena del nivel | Se enlazaron los componentes, se separaron los ladrillos en objetos individuales y se corrigió el fondo duplicado. |
| Escenas de resultado | Se configuraron victoria, derrota y sus botones. En esta última revisión se sustituyeron las imágenes y se añadió el efecto de pulsación desde el Inspector. |

La parte principal de la lógica nueva está en los scripts. También hubo cambios en escenas, prefabs, sonidos y ajustes de interfaz para que esos scripts funcionaran con los objetos del proyecto.

## 2. Botones de victoria y derrota

Los dos botones usan imágenes con marco azul, fondo azul oscuro y letras de píxeles, tomando como referencia los botones PLAY y EXIT del menú. Los textos nuevos son **RETRY** (repetir) y **MENU** (volver al menú). Se usan etiquetas cortas, igual que PLAY y EXIT, para mantener un tamaño de letra parecido.

Las cuatro imágenes están en `Assets/Sprites/BotonesResultado/`:

- `Repetir.png`: botón normal.
- `RepetirPulsado.png`: botón hundido al pulsar.
- `Menu.png`: botón normal.
- `MenuPulsado.png`: botón hundido al pulsar.

Se importaron como **Sprite (2D and UI)**, **Single**, con filtro **Point**, sin mipmaps y sin compresión. El marco y el tamaño del botón se mantienen durante la pulsación; desaparece el brillo superior y las letras bajan ligeramente.

### Cómo se configura en Unity

1. Abrir **Victory** o **Defeat**.
2. Seleccionar el objeto **Repetir** o **Menu** dentro del Canvas.
3. En **Image → Source Image**, asignar la imagen normal. El color debe ser blanco para que Unity respete los colores del sprite.
4. En **Button → Transition**, elegir **Sprite Swap**, igual que en el menú.
5. En **Target Graphic**, usar el componente Image del propio botón.
6. En **Pressed Sprite**, asignar la imagen pulsada correspondiente. Los demás sprites de estado quedan vacíos.
7. En **On Click**, conservar la referencia al objeto **Resultado**: `ResultadoPartida.Repetir` o `ResultadoPartida.VolverMenu`.

Unity hace el cambio de imagen mientras se mantiene pulsado el ratón y recupera la normal al soltar. No se ha añadido ningún script, Animator ni clip de animación para este efecto.

Cada botón mide **300 × 145**, igual que PLAY y EXIT. Los centros están separados **240** unidades, dejando **95** unidades entre los bordes, también igual que en el menú. Los sprites conservan las proporciones 140:68 del marco original. Los botones están anclados al centro y llevan el texto dentro del sprite. Se retiraron sus antiguos objetos Text para evitar que aparecieran dos etiquetas superpuestas.

## 3. Funcionamiento de los scripts

### PaddleMovement: mover la raqueta

En `Update` se lee `Input.GetAxisRaw("Horizontal")`. A/D y las flechas dan izquierda o derecha. Se calcula:

`nueva X = X actual + entrada × velocidad × Time.deltaTime`

`Mathf.Clamp` limita la X para que la raqueta no salga de la zona de juego. Después se asigna la posición al Transform.

### Ball: lanzamiento y rebotes

Antes de lanzar, la bola se coloca encima de la raqueta y la sigue. Espacio llama a `Lanzar`, que fija una dirección inicial ligeramente inclinada.

El movimiento se hace en `LateUpdate`, después del `Update` de la raqueta. Así la bola preparada para lanzar sigue la posición que la raqueta acaba de tomar. La distancia del fotograma es `velocidad × Time.deltaTime`.

Antes de desplazarla, `Physics2D.CircleCast` comprueba todo ese recorrido usando el radio de la bola. Solo consulta la capa **Escenario**, donde están la raqueta, las paredes y los ladrillos. La bola está en **Pelota**, por lo que no se detecta a sí misma.

Si hay un choque:

- Se coloca el centro de la bola en `choque.centroid`, con una separación pequeña para evitar quedarse pegada.
- En paredes y ladrillos se usa `Vector2.Reflect` con la normal del contacto.
- En la raqueta se calcula la zona de impacto: el centro envía la bola hacia arriba y los extremos la desvían.
- La dirección se normaliza para mantener la velocidad.
- Si el objeto tiene `Brick`, se llama a `RecibirGolpe`. También se reproduce el sonido correspondiente.

El bucle aprovecha la distancia que queda si hay otro choque en el mismo fotograma, con un máximo de cuatro rebotes. `Physics2D.SyncTransforms` actualiza los colliders después del movimiento de la raqueta. Se corrigen las direcciones casi horizontales para evitar trayectorias que prolonguen demasiado la partida.

Si la Y de la bola baja del límite inferior, se llama a `GameManager.PerderNivel`. Este movimiento usa Transform y barridos; la bola no lleva Rigidbody2D.

### Brick: destruir solo el ladrillo golpeado

`Awake` inicializa la resistencia. `RecibirGolpe` resta una unidad. En este nivel cada ladrillo tiene una vida y vale **100 puntos**.

Al quedarse sin vida, `BreakBrick` marca el ladrillo como destruido, desactiva su collider, emite `LadrilloDestruido` con los puntos y destruye ese objeto. Desactivar el collider evita que otro barrido lo detecte antes de que Unity termine de eliminarlo.

El evento ya estaba en el script de partida. Se conserva también `OnCollisionEnter2D`, aunque el recorrido actual de la bola llama directamente a `RecibirGolpe`. Los cambios de sprite por daño quedan disponibles para ladrillos de varias vidas; en este nivel no se utilizan.

Hay **33 objetos independientes**, en tres filas de once. Cada uno tiene su collider y su componente Brick. Las paredes y los ladrillos se guardan en la escena; no hay scripts que construyan el escenario durante la partida.

### GameManager: puntos y final de partida

Al comenzar cuenta los objetos con tag **Brick** y actualiza los textos de puntos y bloques. Se suscribe al evento de Brick en `OnEnable` y se desuscribe en `OnDisable`.

Cada destrucción suma los puntos y resta un bloque. Al llegar a cero llama a `Terminar` y carga **Victory** tras 0,3 segundos. Si la bola cae, llama a `Terminar` y carga **Defeat**.

`Terminar` guarda la puntuación en `puntosFinales`, una variable estática que se puede consultar desde la siguiente escena, y desactiva el movimiento de bola y raqueta. La variable `terminado` evita procesar otro final de partida. Completar los 33 ladrillos da **3300 puntos**.

### ResultadoPartida y MenuManager: cambiar de escena

`ResultadoPartida.Start` muestra `GameManager.puntosFinales`. `Repetir` carga de nuevo **Nivel 1**, que empieza con sus 33 ladrillos y puntuación cero. `VolverMenu` carga **Menu**.

`MenuManager.cargarNivel` abre **Nivel 1** y `salirJuego` llama a `Application.Quit`. Salir cierra el juego compilado; no cierra el editor de Unity.

## 4. Otros ajustes del proyecto

- Los Canvas usan **Scale With Screen Size**, referencia **1920 × 1080** y **Match 0,5**. La ventana Game debe revisarse con esa resolución fija.
- El orden de escenas del Build es **Menu**, **Nivel 1**, **Victory**, **Defeat**.
- La cámara del nivel es fija. Se dejó un solo fondo de estrellas: las copias que se solapaban hacían que pareciera cambiar al destruir ladrillos.
- Se añadieron cinco sonidos cortos: lanzamiento, rebote, ladrillo, victoria y derrota.
- Se mantiene un único nivel. No se ha implementado una secuencia de niveles, vidas adicionales ni funciones ajenas a lo solicitado.

## 5. Revisión con los apuntes

Se revisaron los documentos de la carpeta UMU, especialmente:

| Documento | Relación con el proyecto |
| --- | --- |
| `PlanificacionArkanoid.pdf`, página 1 | Menú, nivel, movimiento de raqueta, lanzamiento, rebotes, destrucción y final al caer la bola o no quedar ladrillos. Sonidos y puntuación aparecen como trabajo adicional. |
| `FCV_3_Clase.pdf`, páginas 16–25 del PDF | Movimiento con Transform, entrada por ejes y recomendación de CircleCast2D para Arkanoid. Se usan centroid, Reflect, velocidad constante y dirección según el impacto en la raqueta. |
| `PracticasFCVBloque1.pdf`, página 2 | Canvas, EventSystem, escala 1920 × 1080, importación de sprites y filtro Point para píxeles. |
| `FCV_2_Clase.pdf`, páginas 23–24 del PDF | Cámara y fondo fijos, HUD, colliders 2D, final de partida y explicación del uso de IA. |

La estructura usa componentes de Unity, variables sencillas y métodos cortos. No se añadieron sistemas generales para generar niveles o animar botones. Conviene que cada compañero pueda explicar el recorrido de un golpe: **Ball → Brick → evento → GameManager → HUD o Victory**.

Hay dos diferencias de entrega que deben tenerse presentes: FCV_2 menciona **Unity 6.3**, mientras que este proyecto usa **6.4 (6000.4.12f1)** por la decisión tomada durante su preparación. Además, FCV_2 pide ejecutable de **Windows** y paquete; la planificación dice ejecutable y/o paquete. Se ha preparado el paquete de Unity; el ejecutable de Windows sigue pendiente.

## 6. Comprobación

Se comprobó en modo Play, para los dos botones de las dos escenas:

- La imagen normal cambia a la pulsada al presionar y vuelve al soltar.
- No hay Animator ni etiquetas Text superpuestas.
- El clic en RETRY reinicia el nivel con 33 ladrillos y cero puntos.
- El clic en MENU carga la escena Menu.
- Se conserva la referencia de resolución 1920 × 1080.

Resultado: los cuatro casos pasaron la prueba en Unity 6.4. Se revisó también la apariencia de las dos escenas a 1920 × 1080.

La comprobación se realiza en una copia temporal, sin incorporar scripts de prueba ni herramientas de preparación al juego. Las comprobaciones anteriores cubrieron rebotes, destrucción individual, victoria, derrota, puntuación y estabilidad del fondo.

## 7. Ayuda de IA y generación de imágenes

Se utilizó Codex para añadir Ball y ResultadoPartida, ampliar Brick y GameManager, enlazar las escenas y comprobar el juego. Los botones de resultado se generaron con la herramienta integrada de imágenes, usando los botones existentes como referencia. Esta explicación documenta esa ayuda y permite revisar el código; no atribuye a los alumnos trabajo generado por la herramienta.

### Prompts de las cuatro imágenes

Se generaron con la herramienta integrada de imágenes. Esta segunda revisión utiliza las proporciones del menú original (140:68) y las etiquetas cortas RETRY y MENU para conservar el tamaño de letra. Sustituye a las primeras imágenes, que eran más anchas. Los archivos y sus .meta mantienen el mismo nombre dentro del proyecto.

#### Repetir.png

Referencia: botón PLAY normal del menú.

> Edit this exact PLAY game button. Change only its word to RETRY. Preserve the reference button's original proportions, 140 by 68 logical pixels (aspect ratio 2.0588:1), the exact dark-blue outer frame, almost black navy inset, muted blue-gray top highlight, small white dot at the lower-left, and the existing thin white pixel font. RETRY has five letters, so center it with slightly smaller gaps; keep the same letter height and narrow stroke thickness as PLAY. The reference is the edit target, not inspiration for a redesign. Keep the rectangular frame fixed and all colors matched. Normal unpressed state. One flat opaque sprite filling the canvas exactly. Do not widen the button, brighten the blues, thicken the text, add bevels, gradients, glow, shadows or margins. Keep crisp square pixel steps.

#### RepetirPulsado.png

Referencias: Repetir.png y play 2.jpeg.

> Edit the first image, the RETRY button, to make its PRESSED sprite. The second image is the original PLAY pressed-state reference. Keep the first image's exact canvas size and 140:68 proportions, fixed dark-blue frame, exact RETRY letters and font size. Make the inset look like the second reference: remove the top blue-gray highlight and replace it with the same near-black navy as the inner panel. Shift the word RETRY down by 4 logical pixels, with the same pressed effect as PLAY. Keep the outer frame fixed, keep the tiny white dots in the same places and preserve the colors. Change nothing else. Opaque pixel-art sprite, edge-to-edge, no surrounding margin. No added shading, bevel, blur, glow, gradients or new details.

#### Menu.png

Referencia: botón EXIT normal del menú.

> Edit the reference EXIT game button. Replace only the word EXIT with MENU (four uppercase letters, no accent). Preserve the exact 140 by 68 logical pixel proportions, the fixed muted dark-blue rectangular frame, near-black navy inset, muted blue-gray top highlight, tiny white dot at lower-left and the existing narrow white pixel-font strokes. MENU must have exactly the same cap height and stroke thickness as EXIT; center it. Match the original colors and pixel style faithfully. NORMAL state. One opaque sprite filling the canvas edge to edge. No redesign, no wider button, no thicker letters, no brighter blue, no extra shading, bevels, glow, gradients, external shadows, margin or additional text.

#### MenuPulsado.png

Referencias: Menu.png y exit 2.jpeg.

> Edit the first image, the MENU game button, into its PRESSED state. The second image is the original menu EXIT pressed sprite. Keep the first image's exact canvas size and 140:68 proportions, dark-blue rectangular outer frame, word MENU, narrow pixel font, letter size and horizontal alignment unchanged. Remove the top blue-gray highlight, replacing it with the same dark navy as the inset. Move MENU down by 4 logical pixels, matching the second reference's press effect. Preserve the tiny lower-left white dot and all original colors and border geometry. No other changes. One opaque pixel sprite filling the canvas; no margin, extra text, gradients, glow or new effects.
