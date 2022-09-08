﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equation
{
    public int num;

    private string value;
    private string[] words;

    private bool lastIsNum;
    
    public Equation()
    {
        value = "";
        num = 0;
        lastIsNum = false;
    }

    public Equation(string s)
    {
        value = s;
        num = 0;
        lastIsNum = false;
    }

    // 필드에 있는 유닛을 문자열 수식으로 만든다.
    public void MakeEquation(Vector3 pos, Vector3 dir, bool back)
    {
        while (true) {
            RaycastHit2D hit = Unit.ObjectCheck(pos + dir, 5);

            if (hit) {
                Unit touched = hit.collider.GetComponent<Unit>();
                if (touched.value == null)
                    break;

                touched.Calced();
                AddValue(touched.value, back);

                pos += dir;
            }
            else
                break;
        }

        value = value.Trim();

    }

    public void AddValue(string str, bool addback)
    {
        bool isNum = int.TryParse(str.Substring(0, 1), out int i);

        if (isNum != lastIsNum)
        {
            if (addback)
                value = str + " " + value;
            else
                value = value + " " + str;
        }
        else
        {
            if (addback)
                value = str + value;
            else
                value = value + str;
        }
        lastIsNum = isNum;
        num++;

    }

    public bool TryCalc(out float calcResult) {
        
        if (!CalcAbleCheck()) {
            calcResult = 0;
            return false;
        }

        words = value.Split(' ');

        calcResult = CalculateEquation();

        return true;

    }

    public bool CalcAbleCheck() {

        if (num == 0)
            return false;
        // 첫 문자가 +, -가 아닌 문자일 경우 false

        string str = value.Substring(0, 1);
        bool isNum = int.TryParse(str, out int i);

        if (!isNum && str != "+" && str != "-")
            return false;

        //마지막 문자가 숫자면 true, 아니면 false
        str = value.Substring(value.Length - 1);
        isNum = int.TryParse(str, out i);

        return isNum;
    }

    float CalculateEquation() {

        float[] nums = new float[3];
        string[] ops = new string[2];

        int numsNum = 0;
        int opsNum = 0;
        int wordIdx = 0;
        int length = words.Length;

        if (words[wordIdx] == "-") {
            wordIdx++;
            nums[numsNum++] = -1;
            ops[opsNum++] = "*";
        }
        else if (words[wordIdx] == "+") {
            wordIdx++;
        }

        while (true) {
            if (words[wordIdx] == "(") {
                // 재귀로 실행하면 괄호가 구현될듯 함
            }
            nums[numsNum++] = int.Parse(words[wordIdx++]);

            if (numsNum == 3) {
                if (ops[0] == "^") {
                    nums[0] = Calc(nums[0], ops[0], nums[1]);

                    ops[0] = ops[1];
                    nums[1] = nums[2];
                }
                else if (ops[1] != "+" && ops[1] != "-") {
                    nums[1] = Calc(nums[1], ops[1], nums[2]);
                }
                else {
                    nums[0] = Calc(nums[0], ops[0], nums[1]);

                    ops[0] = ops[1];
                    nums[1] = nums[2];

                }

                numsNum--;
                opsNum--;

            }

            if (length == wordIdx || words[wordIdx] == ")") {
                if (numsNum == 2)
                    nums[0] = Calc(nums[0], ops[0], nums[1]);

                return nums[0];
            }

            ops[opsNum++] = words[wordIdx++];

        }


        // 숫자와 기호를 넣으면 결과를 출력한다.
        float Calc(float Num1, string Char, float Num2)
        {

            if (Char == "+")
                return Num1 + Num2;
            else if (Char == "-")
                return Num1 - Num2;
            else if (Char == "*")
                return Num1 * Num2;
            else if (Char == "^")
                return Mathf.Pow(Num1, Num2);
            else
                return Num1 / Num2;

        }

    }
}