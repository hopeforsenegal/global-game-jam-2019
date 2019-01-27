using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceEnumUtil
{
	public enum PieceEnum
	{
		Cross,
		Line,
		L,
		T,
	}

	public enum DirectionEnum
	{
		Up,
		Down,
		Left,
		Right,
	}

	public static DirectionEnum ToLeft(DirectionEnum e)
	{
		switch (e) {
			case DirectionEnum.Up:
				return DirectionEnum.Left;

			case DirectionEnum.Left:
				return DirectionEnum.Down;

			case DirectionEnum.Down:
				return DirectionEnum.Right;

			case DirectionEnum.Right:
				return DirectionEnum.Up;
		}

		throw new Exception();
	}

	public static DirectionEnum ToRight(DirectionEnum e)
	{
		switch (e) {
			case DirectionEnum.Up:
				return DirectionEnum.Right;

			case DirectionEnum.Right:
				return DirectionEnum.Down;

			case DirectionEnum.Down:
				return DirectionEnum.Left;

			case DirectionEnum.Left:
				return DirectionEnum.Up;
		}
		throw new Exception();
	}


	public static DirectionEnum ToAcross(DirectionEnum e)
	{
		switch (e) {
			case DirectionEnum.Up:
				return DirectionEnum.Down;

			case DirectionEnum.Down:
				return DirectionEnum.Up;

			case DirectionEnum.Left:
				return DirectionEnum.Right;

			case DirectionEnum.Right:
				return DirectionEnum.Left;
		}
		throw new Exception();
	}
}