namespace _3C.Camera
{
    public class TPSCamera : CameraDefault
    {
        #region Movement

        protected override void MoveToTarget()
        {
            if (!IsValidSettings || !settings.CanMove) return;
            transform.position = ComputePosition();
        }

        #endregion

        #region Rotation

        protected override void RotateToTarget()
        {
            if (!IsValidSettings || !settings.CanRotate) return;
            transform.rotation = ComputeRotation();
        }

        #endregion
    }
}
