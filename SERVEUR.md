\# Hébergement sur serveur Linux (VM Ubuntu)



\## Contexte

Déploiement de l'application sur un serveur Linux dédié (VM Ubuntu Server 24.04 LTS

sous VMware Workstation), en complément des environnements Docker Compose local et

Kubernetes, pour valider un hébergement proche des conditions de production.



\## Configuration de la VM

\- OS : Ubuntu 22.04.5 LTS

\- Ressources : 4 Go RAM, 2 vCPU, 30 Go disque

\- Réseau : NAT, adresse IP 192.168.137.131

\- Accès : SSH (openssh-server)



\## Installation réalisée

1\. Création de la VM sous VMware Workstation

2\. Installation d'Ubuntu Server

3\. Configuration du DNS (NetworkManager) pour résoudre les registries Docker

4\. Installation d'OpenSSH Server pour l'administration à distance

5\. Installation de Docker Engine + Docker Compose (dépôt officiel Docker)

6\. Clonage du dépôt applicatif directement sur le serveur (`git clone`)

7\. Déploiement via `docker compose up -d --build`



\## Résultat

Les 3 conteneurs (frontend, backend, base de données PostgreSQL) tournent sur la VM :



\\`\\`\\`

CONTAINER ID   IMAGE                COMMAND                  STATUS

2268c45a986a   devops-sg-frontend   "/docker-entrypoint..."  Up (healthy)

3981bd9cc3ea   devops-sg-backend    "dotnet API.dll"         Up

424ca1c40cd4   postgres:16-alpine   "docker-entrypoint.s…"   Up (healthy)

\\`\\`\\`



Application accessible depuis le réseau local via :

\- Frontend : http://192.168.137.131:8081

\- Backend (API) : http://192.168.137.131:8080



\## Accès SSH

\\`\\`\\`bash

ssh devops@192.168.137.131

\\`\\`\\`

