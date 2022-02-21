###### HOW TO RUN

<pre>From solution root:
	docker build -t username/platformservice
	docker run -p 8080:80 -d username/platformservice

MSSQL in K8:
1 - Create secret key
	kubectl create secret generic mssql --from-literal=SA_PASSWORD='your_password'
2 - Create pvc
3 - Run deployment
</pre>
