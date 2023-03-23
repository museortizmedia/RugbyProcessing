# RugbyProcessing
Este proyecto en Unity integra Processing 3.5.4 con Unity 2020.3.1f1 para desarrollar un video juego que hace tracking de un objeto real en el mundo virtual.
###### Código de processing (debe iniciarse antes de comenzar el juego)
```
//marcador color amarillo

import processing.net.*;

import processing.video.*;

import processing.serial.*;




/***
* El siguiente programa en processing permite hacerle tracking a dos Marcadores pasivos
* que tengan colores diferentes.
*/

//Instancio la variable para capturar la camara
Capture camara;
Server servidor;
String datosPosiciones = "";//Guarda la informacion que se enviara por el puerto.

// Instancio la variable del color que se va a buscar
color marcadorA;
color marcadorB;

int xMarcadorA = 0;
int yMarcadorA = 0;

int xMarcadorB = 0;
int yMarcadorB = 0;

// Distancia de semejanza en color
float semejanzaEnColor = 45;
float minimoDePixelesSemejantes = 50;

void settings() {
    // creo la ventana
    size(640, 480);
}

void setup()
{
    // Iniciar servidor en el puerto 5204
    servidor = new Server(this, 5204);

    // Habilitar solo para depurar el driver de la camara en caso de problemas detectando la camara
    //String[] cameras = Capture.list();
    //printArray(cameras);
    //camara = new Capture(this, cameras[3]);

    //En la variable video almaceno la camra
    camara = new Capture(this, width, height, 30);
    camara.start();

    // Marcadores
    marcadorA = color(180,255,126); // Color real del marcador A.
    //marcadorB = color(82,145,119); // Color real del marcador B.
}

void draw()
{
    //verifica si la camara esta disponible
    if (camara.available())
    {
        camara.read();
        image(camara, 0, 0);
        camara.loadPixels();

        float promedioXMarcadorA = 0;
        float promedioYMarcadorA = 0;

        float promedioXMarcadorB = 0;
        float promedioYMarcadorB = 0;

        int cantidadPixelesCoincidenConMarcadorA = 0;
        int cantidadPixelesCoincidenConMarcadorB = 0;

        //empieza a recorrer cada pixel
        for ( int x = 0; x < camara.width; x++ )
        {
            for ( int y = 0; y < camara.height; y++ )
            {

                color pixelActual = camara.pixels[x + y * camara.width];

                float cantidadRojoDelPixelActual = red(pixelActual);
                float cantidadVerdeDelPixelActual = green(pixelActual);
                float cantidadAzulDelPixelActual = blue(pixelActual);

                float cantidadRojoDelMarcadorA = red(marcadorA);
                float cantidadVerdeDelMarcadorA = green(marcadorA);
                float cantidadAzulDelMarcadorA = blue(marcadorA);

                // Calculando la distancia de similitud en "color" para el marcador A.
                float similitudEnDistanciaDelColorMarcadorA = dist(cantidadRojoDelPixelActual, cantidadVerdeDelPixelActual, cantidadAzulDelPixelActual, cantidadRojoDelMarcadorA, cantidadVerdeDelMarcadorA, cantidadAzulDelMarcadorA); // We are using the dist( ) function to compare the current color with the color we are tracking.

                // Esta muy cerca del rojo
                if (similitudEnDistanciaDelColorMarcadorA < semejanzaEnColor)
                {
                    promedioXMarcadorA += x;
                    promedioYMarcadorA += y;
                    cantidadPixelesCoincidenConMarcadorA++;
                } else {

                    float cantidadRojoDelMarcadorB = red(marcadorB);
                    float cantidadVerdeDelMarcadorB = green(marcadorB);
                    float cantidadAzulDelMarcadorB = blue(marcadorB);

                    float similitudEnDistanciaDelColorMarcadorB = dist(cantidadRojoDelPixelActual, cantidadVerdeDelPixelActual, cantidadAzulDelPixelActual, cantidadRojoDelMarcadorB, cantidadVerdeDelMarcadorB, cantidadAzulDelMarcadorB); // We are using the dist( ) function to compare the current color with the color we are tracking.

                    // Esta muy cerca del verde
                    if (similitudEnDistanciaDelColorMarcadorB < semejanzaEnColor)
                    {
                    promedioXMarcadorB += x;
                    promedioYMarcadorB += y;
                    cantidadPixelesCoincidenConMarcadorB++;
                    }

                }
            }
        }

        if ( cantidadPixelesCoincidenConMarcadorA > minimoDePixelesSemejantes )
        {
            xMarcadorA = (int) promedioXMarcadorA / cantidadPixelesCoincidenConMarcadorA;
            yMarcadorA = (int) promedioYMarcadorA / cantidadPixelesCoincidenConMarcadorA;
        }

        /*if ( cantidadPixelesCoincidenConMarcadorB > minimoDePixelesSemejantes )
        {
            xMarcadorB = (int) promedioXMarcadorB / cantidadPixelesCoincidenConMarcadorB;
            yMarcadorB = (int) promedioYMarcadorB / cantidadPixelesCoincidenConMarcadorB;
        }*/

        dibujarCentroide(marcadorA, xMarcadorA, yMarcadorA);
        //dibujarCentroide(marcadorB, xMarcadorB, yMarcadorB);

        if (xMarcadorA > 0 || yMarcadorA > 0 || xMarcadorB > 0 || yMarcadorB > 0) {
            datosPosiciones = (width-xMarcadorA)+","+(height-yMarcadorA)+","+(width-xMarcadorB)+","+(height-yMarcadorB)+"\n";
        } else {
            datosPosiciones = "0,0,0,0\n";
        }
        //Enviar el dato por el puerto
        servidor.write(datosPosiciones);
    }
}

/**
* Dibujar centroide.
*/
void dibujarCentroide (color marcador, int xMarcador, int yMarcador) {
    fill(marcador);
    strokeWeight(4.0);
    stroke(0);
    ellipse(xMarcador, yMarcador, 16, 16);
}

/**
* Obtiene el color exacto del pixel donde
* se dio un click.
*/
void mousePressed() {

    int loc = mouseX + mouseY * camara.width;
    color pixelLeido = camara.pixels[loc];

    float r1 = red(pixelLeido);
    float g1 = green(pixelLeido);
    float b1 = blue(pixelLeido);
    print(r1 + " "+ g1+ " "+b1+ "\n");

    //Habilitar para hacer tracking
    //marcadorRojo = pixelLeido;
}
```

## Licencia
Los gráficos usados en este proyecto NO NOS PERTENECEN, hacen parte del videojuego Rugby Rush de Code This Lab S.r.l. para Android. Los audios son extraidos de librerías libres. El código es autoría propia y el fragmento de processing es librería de la Universidad Autónoma de Occidente.
