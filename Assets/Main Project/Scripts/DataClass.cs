using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtomClass{
	public DataClass[] atomData;
	public AtomClass(int noOfAtoms){
		atomData = new DataClass[noOfAtoms];
	}

}

public class DataClass{
	public string atomName;
	public string iconUrl;
	public int noOfElectrons;
	public int noOfProtrons;
	public int noOfNutrons;
	public int matrixSize;
}