FROM boxfuse/flyway
WORKDIR /src
COPY Migrations/sql .
ENTRYPOINT flyway migrate -user=$SA_USER -password=$SA_PASSWORD -url="jdbc:sqlserver://db:1433;databaseName=Ticketing" -locations="filesystem:."