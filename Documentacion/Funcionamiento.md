# Funcionamiento de Arkanoid

El juego usa el nivel que ya estaba colocado en la escena: sus paredes, sus tres filas de ladrillos y la raqueta. El script `PaddleMovement` sigue controlando la raqueta con el eje Horizontal.

## Bola

Antes de lanzar, la bola sigue a la raqueta. Espacio activa el movimiento. En cada fotograma se calcula la distancia como velocidad por tiempo y se hace un `CircleCast2D` sobre ese recorrido para comprobar si hay un obstáculo.

Si hay un choque, la bola se coloca en el punto de contacto y cambia de dirección con `Vector2.Reflect`. En la raqueta, la dirección depende de dónde toque: el centro la manda hacia arriba y los extremos la desvían hacia los lados. La velocidad se mantiene constante. Solo se consultan los colliders de la capa Escenario, para que la bola no se detecte a sí misma.

## Ladrillos y partida

`Brick.RecibirGolpe` resta resistencia al bloque. Al llegar a cero desactiva el collider, avisa mediante el evento `LadrilloDestruido` y elimina el objeto. `GameManager` escucha ese evento, suma los puntos y resta un bloque. Las paredes no llevan el componente `Brick`.

Cuando quedan cero bloques se carga Victory. Si la bola baja de la zona de juego se carga Defeat. La puntuación final se guarda en una variable estática para mostrarla en la pantalla de resultado. Los botones de esa pantalla cargan otra vez Nivel 1 o vuelven a Menu.

El nivel contiene 33 ladrillos independientes en tres filas de 11. Cada objeto tiene su componente `Brick` y su collider, por lo que solo desaparece el ladrillo golpeado. Cada uno da 100 puntos y la victoria llega cuando no queda ninguno. Los ladrillos están guardados en la escena; no se generan paredes ni ladrillos durante la partida.

## Sonidos y resolución

Hay efectos cortos para lanzar, rebotar, romper un bloque, ganar y perder. Son tonos generados para este proyecto. La interfaz toma 1920 × 1080 como resolución de referencia y escala con el tamaño de la pantalla. El nivel tiene un único fondo de estrellas detrás de los objetos del juego, para que no cambie la imagen al destruir ladrillos.

## Uso de IA

Se ha usado Codex para ayudar con la bola, ampliar la lógica de partida, enlazar las escenas, configurar la interfaz y comprobar el funcionamiento. Esta explicación permite al grupo revisar el código y entender los cambios antes de entregar.
