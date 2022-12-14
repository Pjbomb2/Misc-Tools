using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatrixSetter : MonoBehaviour
{
    public Quaternion ExtractRotation(Matrix4x4 matrix)
    {
        Vector3 forward;
        forward.x = matrix.m02;
        forward.y = matrix.m12;
        forward.z = matrix.m22;
 
        Vector3 upwards;
        upwards.x = matrix.m01;
        upwards.y = matrix.m11;
        upwards.z = matrix.m21;
 
        return Quaternion.LookRotation(forward, upwards);
    }
 
    public Vector3 ExtractPosition(Matrix4x4 matrix)
    {
        Vector3 position;
        position.x = matrix.m03;
        position.y = matrix.m13;
        position.z = matrix.m23;
        return position;
    }
 
    public Vector3 ExtractScale(Matrix4x4 matrix)
    {
        Vector3 scale;
        scale.x = new Vector4(matrix.m00, matrix.m10, matrix.m20, matrix.m30).magnitude;
        scale.y = new Vector4(matrix.m01, matrix.m11, matrix.m21, matrix.m31).magnitude;
        scale.z = new Vector4(matrix.m02, matrix.m12, matrix.m22, matrix.m32).magnitude;
        return scale;
    }

    public void OnDrawGizmos() {
        Matrix4x4 ThisMatrix = Matrix4x4.zero;
        // Vector4 A = new Vector4(4.37114e-008f, 8.74228e-008f, -2, -1);
        // Vector4 B = new Vector4(1, 3.82137e-015f, -8.74228e-008f, 1);
        // Vector4 C = new Vector4(0, -1, -4.37114e-008f, 0);
        // Vector4 D = new Vector4(0, 0, 0, 1);

        Vector4 A = new Vector4(0.235f, -1.66103e-008f, -7.80685e-009f, -0.005f);
        Vector4 B = new Vector4(-2.05444e-008f, 3.90343e-009f, -0.0893f, 1.98f);
        Vector4 C = new Vector4(2.05444e-008f, 0.19f, 8.30516e-009f, -0.03f);
        Vector4 D = new Vector4(0, 0, 0, 1);

        ThisMatrix.SetRow(0, A);
        ThisMatrix.SetRow(1, B);
        ThisMatrix.SetRow(2, C);
        ThisMatrix.SetRow(3, D);
        this.transform.localScale = ExtractScale(ThisMatrix);
        this.transform.localRotation = ExtractRotation(ThisMatrix);
        this.transform.localPosition = ExtractPosition(ThisMatrix) * 0.5f;

    }
}
