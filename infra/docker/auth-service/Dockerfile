# syntax=docker/dockerfile:1
FROM dhi.io/maven:3-jdk25-debian-dev AS build
WORKDIR /src
COPY pom.xml .
RUN --mount=type=cache,id=maven,target=/root/.m2,sharing=locked mvn -q dependency:go-offline
COPY src ./src
RUN --mount=type=cache,id=maven,target=/root/.m2,sharing=locked mvn -q package -DskipTests

FROM eclipse-temurin:25-jre-alpine AS extract
WORKDIR /build
COPY --from=build /src/target/*.jar app.jar
RUN java -Djarmode=tools -jar app.jar extract --layers --destination extracted

FROM eclipse-temurin:25-jre-alpine AS runtime
RUN apk add --no-cache curl \
 && addgroup -S -g 10001 app \
 && adduser -S -D -H -u 10001 -G app -s /sbin/nologin app
WORKDIR /app
COPY --from=extract /build/extracted/dependencies/ ./
COPY --from=extract /build/extracted/spring-boot-loader/ ./
COPY --from=extract /build/extracted/snapshot-dependencies/ ./
COPY --from=extract /build/extracted/application/ ./
USER app
EXPOSE 8080
ENTRYPOINT ["java", "-XX:MaxRAMPercentage=75.0", "-jar", "app.jar"]
