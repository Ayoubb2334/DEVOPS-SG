\# Infrastructure DevOps — Smartphone App



\## Déploiement de l'application (Helm)



kubectl create namespace smartphone-app

helm install mon-app ./smartphone-app-chart



\## Déploiement du monitoring (Prometheus + Grafana)



helm repo add prometheus-community https://prometheus-community.github.io/helm-charts

helm repo update

kubectl create namespace monitoring

helm install monitoring prometheus-community/kube-prometheus-stack -n monitoring -f monitoring/values-monitoring.yaml --timeout 15m



\## Déploiement GitOps (ArgoCD)



kubectl create namespace argocd

kubectl apply -n argocd -f https://raw.githubusercontent.com/argoproj/argo-cd/stable/manifests/install.yaml --server-side

kubectl apply -f gitops/argocd-application.yaml



\## Pipeline CI/CD



Voir le fichier .gitlab-ci.yml à la racine du projet.

