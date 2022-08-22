using Emgu.CV;
using Emgu.CV.Aruco;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using ExclusiveProgram.device;
using PuzzleLibrary.puzzle.visual.concrete;
using PuzzleLibrary.puzzle;
using RASDK.Arm;
using RASDK.Basic;
using RASDK.Vision.IDS;
using RASDK.Vision.Positioning;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ExclusiveProgram.puzzle
{
    struct Region{
        public Rectangle ROI;
        public double[] capture_position;
        public string positioning_filepath;
    }
    public class PuzzleHandler
    {
        private static double PICK_Z_POSITION = 3;
        private static double TARGET_Z_POSITION = 40;
        private static int SUCKER_DISABLE_DELAY=400;
        private static int SUCKER_ENABLE_DELAY=400;
        private static double PUT_Z_POSITION=0.655;
        private readonly IPuzzleFactory factory;
        private readonly RoboticArm arm;
        private readonly IDSCamera camera;
        private readonly SuckerDevice sucker;
        private readonly Dictionary<Point, PointF> put_positions;


        public PuzzleHandler(IPuzzleFactory factory,RoboticArm arm,IDSCamera camera,SuckerDevice sucker)
        {
            this.factory = factory;
            this.arm = arm;
            this.camera = camera;
            this.sucker = sucker;
            put_positions = ReadPutPositionsFromFile("positioning\\put_positions.csv");
        }

        private Dictionary<Point,PointF> ReadPutPositionsFromFile(string filepath)
        {
            var dictionary = new Dictionary<Point, PointF>();
            var datalist=Csv.Read(filepath);
            foreach(var list in datalist)
            {
                var array = list[1].ToCharArray();
                var position = new Point(int.Parse(array[1]+""),int.Parse(array[0]+""));
                dictionary.Add(position,new PointF(float.Parse(list[2]), float.Parse(list[3])));
            }
            return dictionary;
        }


        private static Region GetRegion(int index)
        {
            if (index == 1)
            {
                Region region1 = new Region();
                region1.ROI= Rectangle.FromLTRB(1068, 30, 2440, 1999);
                region1.capture_position= new double[] { 195.351, 368.003, 230.336, 180, 0, 90 };
                region1.positioning_filepath = "positioning\\region1_ccia.csv";
                return region1;
            }else if (index == 2)
            {

                Region region2 = new Region();
                region2.ROI= Rectangle.FromLTRB(683,3,2362,2027);
                region2.capture_position= new double[] { -195.351, 368.003, 230.336, 180, 0, 90 };
                region2.positioning_filepath = "positioning\\region2_ccia.csv";
                return region2;
            }
            throw new Exception();
        }
        private Region MoveToRegion(int index)
        {
            var region = GetRegion(index);
            Move(region.capture_position[0], region.capture_position[1], region.capture_position[2]);
            RotateToAngle(0);
            return region;
        }

        public List<Puzzle3D> MoveToRegionAndGetPuzzles(int index,int IDOfStart=0)
        {
            var puzzles=new List<Puzzle3D>();
            var region = MoveToRegion(index);
            var image = CaptureImage();
            puzzles.AddRange(factory.Execute(image,region.ROI,new RASDKPositionerAdaptor(CCIA.LoadFromCsv(region.positioning_filepath)),IDOfStart));
            return puzzles;
        }
        private  Image<Bgr,byte> CaptureImage()
        {
            return camera.GetImage().ToImage<Bgr, byte>();
        }

        public void PickPuzzle(Puzzle3D puzzle)
        {
            Move(puzzle.RealWorldCoordinate.X, puzzle.RealWorldCoordinate.Y, TARGET_Z_POSITION);
            RotateToAngle(puzzle.Angle+90);
            Move(puzzle.RealWorldCoordinate.X, puzzle.RealWorldCoordinate.Y, PICK_Z_POSITION);
            sucker.Enable();
            Thread.Sleep(SUCKER_ENABLE_DELAY);
            Move(puzzle.RealWorldCoordinate.X, puzzle.RealWorldCoordinate.Y, TARGET_Z_POSITION);
            RotateToAngle(0);
        }
        public void PutPuzzle(Puzzle3D puzzle)
        {
            var success=put_positions.TryGetValue(puzzle.Position,out var coordinate);
            if (!success)
                throw new Exception();
            Move(coordinate.X, coordinate.Y, TARGET_Z_POSITION);
            Move(coordinate.X, coordinate.Y, PUT_Z_POSITION);
            sucker.Disable();
            Thread.Sleep(SUCKER_DISABLE_DELAY);
            Move(coordinate.X, coordinate.Y, TARGET_Z_POSITION);
        }

        public void DropPuzzle()
        {
            sucker.Disable();
        }

        public void Move(double x,double y, double z)
        {
            var current = CurrentPosition();
            arm.MoveAbsolute(new double[] { x, y, z, current[3], current[4], current[5] }, new AdditionalMotionParameters { CoordinateType = RASDK.Arm.Type.CoordinateType.Descartes, NeedWait = true});
        }

        public double[] CurrentPosition()
        {
            return arm.GetNowPosition();
        }

        public void BBB(Puzzle3D puzzle)
        {
            var region=MoveToRegion(1);

        }

        public void RotateToAngle(double angle)
        {
            var robotAngle = 90 - angle;
            var position = CurrentPosition();
            position[5] = robotAngle;
            arm.MoveAbsolute(position, new AdditionalMotionParameters { CoordinateType = RASDK.Arm.Type.CoordinateType.Descartes, NeedWait = true});
        }
        public void AAA()
        {

            var image = camera.GetImage().ToImage<Bgr,byte>();
            image.Save("GGG.jpg");
            double angle = 0;
            Func<PointF> func2 = () =>
            {
                var img = camera.GetImage().ToImage<Bgr,byte>();
                var cor = new VectorOfVectorOfPointF();
                var ids = new VectorOfInt();
                ArucoInvoke.DetectMarkers(img.Mat, new Dictionary(Dictionary.PredefinedDictionaryName.Dict4X4_50),cor, ids, DetectorParameters.GetDefault());

                var id_array = ids.ToArray();
                for(int i = 0; i < id_array.Length; i++)
                {
                    var id=id_array[i];
                    if(id == 0)
                    {
                        PointF[] points= cor.ToArrayOfArray()[i];
                        //Top left
                        var topleft= points[0];
                        var topright= points[1];
                        var bottomright= points[2];
                        var bottomleft= points[3];

                        angle = Math.Atan2(topleft.Y-topright.Y,topright.X-topleft.X)*(180/Math.PI);

                        for(int p = 0; p < points.Length; p++)
                        {
                            CvInvoke.PutText(img,p+"",new Point((int)points[p].X,(int)points[p].Y-10),Emgu.CV.CvEnum.FontFace.HersheyPlain,2,new MCvScalar(255,0,0));
                            CvInvoke.Circle(img, Point.Round(points[p]),6,new MCvScalar(255,0,0),-1);
                        }

                        CvInvoke.PutText(img,"Angle:"+angle,new Point(100,100),Emgu.CV.CvEnum.FontFace.HersheyPlain,5,new MCvScalar(255,0,0));
                        CvInvoke.Polylines(img,Array.ConvertAll<PointF,Point>(points,Point.Round),true,new MCvScalar(255,0,0),5);
                        img.Save("GGG.jpg");
                        return topleft;
                    }

                }


                throw new Exception();

            };

            var kp = (20.0 / 130.0) * 0.8;
            //var func = VisualServo.BasicArmMoveFunc(arm,kp);
            //VisualServo.Tracking(image.Size, 10, func2, func); 
            //var position=arm.GetNowPosition();
            //var pa = new PointF((float)position[0], (float)position[1] );
            //var pc = new PointF(pa.X, (pa.Y + 111.83f));

            //var _points = new PointF[] {pc };
            //var rotateMatrix = new RotationMatrix2D();
            //CvInvoke.GetRotationMatrix2D(pc,angle, 1,rotateMatrix);
            //rotateMatrix.RotatePoints(_points);
            //Move(_points[0].X, _points[0].Y, position[2]);
            
        }
    }
}