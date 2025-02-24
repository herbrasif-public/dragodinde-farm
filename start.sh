#!/bin/bash

# Nom du conteneur et nom de l'image
CONTAINER_NAME="dragodindefarm"
IMAGE_NAME="dragodindefarm"

# Chemin vers le Dockerfile
DOCKERFILE_PATH="."

# Reconstruit l'image Docker
echo "Reconstruction de l'image Docker..."
docker build -t $IMAGE_NAME $DOCKERFILE_PATH

# Vérifie si le conteneur est en cours d'exécution et arrête et supprime le conteneur existant s'il y a lieu
if [ $(docker ps -q -f name=$CONTAINER_NAME) ]; then
    echo "Arrêt du conteneur existant..."
    docker stop $CONTAINER_NAME
    docker rm $CONTAINER_NAME
elif [ $(docker ps -a -q -f name=$CONTAINER_NAME) ]; then
    echo "Suppression du conteneur existant arrêté..."
    docker rm $CONTAINER_NAME
fi

# Lance un nouveau conteneur
echo "Lancement du nouveau conteneur..."
docker run --name $CONTAINER_NAME -d -p 8080:80 $IMAGE_NAME

echo "Le conteneur $CONTAINER_NAME est lancé."
