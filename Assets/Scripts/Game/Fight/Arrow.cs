using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Universal.Collections;
using Universal.Events;

namespace Game.Fight
{
    public class Arrow : StaticPoolableObject
    {
        #region fields & properties
        private Vector3 startPos;
        private Vector3 targetPos;
        private float flightDuration;
        private float arcHeight;
        private float elapsedTime;
        private float fixedZ;
        #endregion fields & properties

        #region methods
        public void Initialize(Vector3 target, float duration, float height)
        {
            startPos = transform.position;
            targetPos = target;

            flightDuration = duration;
            arcHeight = height;
            elapsedTime = 0f;

            fixedZ = transform.position.z;
            targetPos.z = fixedZ;
        }
        public void Update()
        {
            if (elapsedTime >= flightDuration)
            {
                DisableObject();
                return;
            }

            elapsedTime += Time.deltaTime;
            float t = elapsedTime / flightDuration;
            Vector3 currentPosition = Vector3.Lerp(startPos, targetPos, t);
            currentPosition.y += arcHeight * Mathf.Sin(t * Mathf.PI);
            currentPosition.z = fixedZ;

            transform.position = currentPosition;

            float next_t = Mathf.Min(t + 0.01f, 1.0f);
            Vector3 nextPosition = Vector3.Lerp(startPos, targetPos, next_t);
            nextPosition.y += arcHeight * Mathf.Sin(next_t * Mathf.PI);
            nextPosition.z = fixedZ;

            Vector2 direction2D = (nextPosition - currentPosition);

            if (direction2D != Vector2.zero)
            {
                float angle = Mathf.Atan2(direction2D.y, direction2D.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0, 0, angle);
            }
            #endregion methods
        }
    }
}