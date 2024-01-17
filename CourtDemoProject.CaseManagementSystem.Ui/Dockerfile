# Step 1: Install dependencies
FROM node:alpine as builder
WORKDIR /app

# Correct the paths for package.json and package-lock.json
COPY CourtDemoProject.CaseManagementSystem.Ui/package.json CourtDemoProject.CaseManagementSystem.Ui/package-lock.json ./

RUN npm install

# Step 2: Copy app files and build
# Correct the path for copying the application files
COPY CourtDemoProject.CaseManagementSystem.Ui/ ./

RUN npm run build

# Step 3: Start the app
FROM node:alpine
WORKDIR /app
COPY --from=builder /app ./

CMD ["npm", "run", "start"]
