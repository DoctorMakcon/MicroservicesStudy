###### HOW TO RUN

<pre>
Docker:
	docker build -t username/platformservice .
	docker run -p 8080:80 -d username/platformservice

MSSQL in K8:
1 - Create secret key
	kubectl create secret generic mssql --from-literal=SA_PASSWORD="your_password"
	your_password should have 8 or more symbols length(pa55w0rd!)
2 - Create pvc
	kubectl apply -f local-pvc.yaml
3 - Run deployment
	kubectl apply -f mssql-platform-deploy.yaml

Diagnostic:
	kubectl describe pod podname
	kubectl logs podname
</pre>
