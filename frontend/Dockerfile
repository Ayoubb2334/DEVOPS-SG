# ---- Stage 1 : Build ----
FROM node:18-alpine AS build
WORKDIR /app

# Copier uniquement package*.json d'abord (optimisation du cache Docker)
COPY package.json package-lock.json ./
RUN npm ci

# Copier le reste du code source
COPY . .

# Build de production
RUN npm run build -- --configuration production

# ---- Stage 2 : Runtime (Nginx) ----
FROM nginx:alpine AS runtime

# Supprimer la config Nginx par défaut et copier la nôtre
RUN rm /etc/nginx/conf.d/default.conf
COPY nginx.conf /etc/nginx/conf.d/default.conf

# Copier les fichiers buildés d'Angular vers le dossier servi par Nginx
COPY --from=build /app/dist/frontend /usr/share/nginx/html

EXPOSE 80

CMD ["nginx", "-g", "daemon off;"]