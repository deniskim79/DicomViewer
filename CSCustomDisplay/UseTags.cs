using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSCustomDisplay
{
    public class UseTags
    {
        public enum eTagName
        {
            // 0x0002
            eTransferSyntaxUID,

            // 0x0008
            eSpecificCharacterSet,
            eImageType,
            eSOPInstanceUID,
            eStudyDate,
            eSeriesDate,
            eContentDate,
            eStudyTime,
            eSeriesTime,
            eContentTime,
            eAccessionNumber,
            eModality,
            eInstitutionName,
            eReferringPhysicianName,
            eStationName,
            eStudyDescription,
            eSeriesDescription,
            eInstitutionalDepartmentName,
            eOperatorsName,
            eManufacturerModelName,

            // 0x0010
            ePatientName,
            ePatientID,
            ePatientBirthDate,
            ePatientSex,
            ePatientBirthName,
            ePatientAge,
            ePatientComments,
            
            // 0x0018
            eContrastBolusAgent,
            eBodyPartExamined,
            eSliceThickness,
            eKVP,
            eRepetitionTime,
            eEchoTime,
            eInversionTime,
            eSpacingBetweenSlices,
            ePercentPhaseFieldOfView,
            ePlateID,
            eSoftwareVersions,
            eProtocolName,
            eReconstructionDiameter,
            eEstimatedRadiographicMagnificationFactor,
            eTableHeight,
            eExposureTime,
            eXRayTubeCurrent,
            eExposure,
            eImagerPixelSpacing,
            eReceiveCoilName,
            eAcquisitionMatrix,
            ePatientPosition,
            eViewPosition,
            eSensitivity,
            ePhysicalDeltaX,
            ePhysicalDeltaY,

            // 0x0020
            eStudyInstanceUID,
            eSeriesInstanceUID,
            eSeriesNumber,
            eAcquisitionNumber,
            eInstanceNumber,
            eImagePositionPatient,
            eImageOrientationPatient,
            eSliceLocation,
            eImageComments,

            // 0x0028
            ePhotometricInterpretation,
            eNumberOfFrames,
            eRows,
            eColumns,
            ePixelSpacing,
            eBitsAllocated,
            eBitsStored,
            eWindowCenter,
            eWindowWidth,
            eRescaleIntercept,
            eRescaleSlope,

            // 0x0032
            eRequestingPhysician,




            // 전체 Dicom Tag 개수 확인용
            eAllTagCount
        };


        public static Dicom.DicomTag[] UseDcmTag = new Dicom.DicomTag[(int)eTagName.eAllTagCount]
        {

	        // 0x0002
	        Dicom.DicomTag.TransferSyntaxUID,

	        // 0x0008
	        Dicom.DicomTag.SpecificCharacterSet,
            Dicom.DicomTag.ImageType,
            Dicom.DicomTag.SOPInstanceUID,
            Dicom.DicomTag.StudyDate,
            Dicom.DicomTag.SeriesDate,
            Dicom.DicomTag.ContentDate,
            Dicom.DicomTag.StudyTime,
            Dicom.DicomTag.SeriesTime,
            Dicom.DicomTag.ContentTime,
            Dicom.DicomTag.AccessionNumber,
            Dicom.DicomTag.Modality,
            Dicom.DicomTag.InstitutionName,
            Dicom.DicomTag.ReferringPhysicianName,
            Dicom.DicomTag.StationName,
            Dicom.DicomTag.StudyDescription,
            Dicom.DicomTag.SeriesDescription,
            Dicom.DicomTag.InstitutionalDepartmentName,
            Dicom.DicomTag.OperatorsName,
            Dicom.DicomTag.ManufacturerModelName,
            
	        // 0x0010
	        Dicom.DicomTag.PatientName,
            Dicom.DicomTag.PatientID,
            Dicom.DicomTag.PatientBirthDate,
            Dicom.DicomTag.PatientSex,
            Dicom.DicomTag.PatientBirthName,
            Dicom.DicomTag.PatientAge,
            Dicom.DicomTag.PatientComments,
            
	        // 0x0018
	        Dicom.DicomTag.ContrastBolusAgent,
            Dicom.DicomTag.BodyPartExamined,
            Dicom.DicomTag.SliceThickness,
            Dicom.DicomTag.KVP,
            Dicom.DicomTag.RepetitionTime,
            Dicom.DicomTag.EchoTime,
            Dicom.DicomTag.InversionTime,
            Dicom.DicomTag.SpacingBetweenSlices,
            Dicom.DicomTag.PercentPhaseFieldOfView,
            Dicom.DicomTag.PlateID,
            Dicom.DicomTag.SoftwareVersions,
            Dicom.DicomTag.ProtocolName,
            Dicom.DicomTag.ReconstructionDiameter,
            Dicom.DicomTag.EstimatedRadiographicMagnificationFactor,
            Dicom.DicomTag.TableHeight,
            Dicom.DicomTag.ExposureTime,
            Dicom.DicomTag.XRayTubeCurrent,
            Dicom.DicomTag.Exposure,
            Dicom.DicomTag.ImagerPixelSpacing,
            Dicom.DicomTag.ReceiveCoilName,
            Dicom.DicomTag.AcquisitionMatrix,
            Dicom.DicomTag.PatientPosition,
            Dicom.DicomTag.ViewPosition,
            Dicom.DicomTag.Sensitivity,
            Dicom.DicomTag.PhysicalDeltaX,
            Dicom.DicomTag.PhysicalDeltaY,
            
	        // 0x0020
	        Dicom.DicomTag.StudyInstanceUID,
            Dicom.DicomTag.SeriesInstanceUID,
            Dicom.DicomTag.SeriesNumber,
            Dicom.DicomTag.AcquisitionNumber,
            Dicom.DicomTag.InstanceNumber,
            Dicom.DicomTag.ImagePositionPatient,
            Dicom.DicomTag.ImageOrientationPatient,
            Dicom.DicomTag.SliceLocation,
            Dicom.DicomTag.ImageComments,

	        // 0x0028
	        Dicom.DicomTag.PhotometricInterpretation,
            Dicom.DicomTag.NumberOfFrames,
            Dicom.DicomTag.Rows,
            Dicom.DicomTag.Columns,
            Dicom.DicomTag.PixelSpacing,
            Dicom.DicomTag.BitsAllocated,
            Dicom.DicomTag.BitsStored,
            Dicom.DicomTag.WindowCenter,
            Dicom.DicomTag.WindowWidth,
            Dicom.DicomTag.RescaleIntercept,
            Dicom.DicomTag.RescaleSlope,

	        // 0x0032
	        Dicom.DicomTag.RequestingPhysician
        };
    }
}
