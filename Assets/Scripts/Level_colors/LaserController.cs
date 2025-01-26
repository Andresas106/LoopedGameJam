using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserController : MonoBehaviour
{
    public GameObject laser; // Objeto del láser
    public LineRenderer lineRenderer; // Componente LineRenderer del láser
    public float laserLength = 10f; // Longitud máxima del láser
    public Transform laserStart; // Punto inicial del láser
    public enum Direction { Izquierda, Derecha, Arriba, Abajo }
    public Direction selectedDirection; // Dirección elegida por el jugador

    private void Start()
    {
        // Configurar la posición inicial del láser
        lineRenderer.positionCount = 2;
        UpdateLaser(laserStart.position, laserStart.position + Vector3.left * laserLength);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Guardar la posición donde el jugador murió
            Vector3 deathPosition = other.transform.position;

            // Cambiar la dirección del láser en el punto de muerte
            ChangeLaserDirection(deathPosition);
        }
    }

    private void ChangeLaserDirection(Vector3 deathPosition)
    {
        // Calcular la posición relativa dentro del láser
        Vector3 startPosition = laserStart.position;
        Vector3 directionToDeath = (deathPosition - startPosition).normalized;
        float distanceToDeath = Vector3.Distance(startPosition, deathPosition);

        // Actualizar el primer tramo del láser (hasta el punto de muerte)
        UpdateLaser(startPosition, deathPosition);

        // Calcular la nueva dirección basada en la elección del jugador
        Vector3 newDirection = Vector3.zero;
        switch (selectedDirection)
        {
            case Direction.Arriba:
                newDirection = Vector3.left;
                break;
            case Direction.Abajo:
                newDirection = Vector3.right;
                break;
            case Direction.Derecha:
                newDirection = Vector3.forward;
                break;
            case Direction.Izquierda:
                newDirection = Vector3.back;
                break;
        }

        // Añadir el segundo tramo del láser con la nueva dirección
        Vector3 newLaserEnd = deathPosition + newDirection * (laserLength - distanceToDeath);
        lineRenderer.positionCount = 3; // Añadir un tercer punto
        lineRenderer.SetPosition(2, newLaserEnd);
    }

    private void UpdateLaser(Vector3 start, Vector3 intendedEnd)
    {
        // Crea un raycast desde el punto inicial hacia la dirección del láser
        RaycastHit hit;
        Vector3 direction = (intendedEnd - start).normalized;
        float maxDistance = Vector3.Distance(start, intendedEnd);

        if (Physics.Raycast(start, direction, out hit, maxDistance))
        {
            // Si se detecta un obstáculo, corta el láser en ese punto
            lineRenderer.positionCount = 2; // Asegúrate de tener 2 puntos
            lineRenderer.SetPosition(0, start);
            lineRenderer.SetPosition(1, hit.point);
        }
        else
        {
            // Si no hay obstáculos, traza el láser normalmente
            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, start);
            lineRenderer.SetPosition(1, intendedEnd);
        }
    }
}
