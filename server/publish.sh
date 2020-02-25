echo 'Building Docker Container'
docker build -t classroom-service .

echo 'Tagging Container for Quay Repo'
docker tag classroom-service quay.io/jzywien/classroom-service

echo 'Pushing to Quay repo'
docker push quay.io/jzywien/classroom-service  