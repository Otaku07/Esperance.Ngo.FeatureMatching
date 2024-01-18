using OpenCvSharp;

namespace Esperance.Ngo.FeatureMatching.csproj;

public class ObjectDetection 
{ 
    
    public async Task<List<ObjectDetectionResult>> DetectObjectInScenesAsync(byte[] imageObjectData, List<byte[]> imageScenesData)
    {
        var tasks = imageScenesData.Select(imageSceneData => Task.Run(() => DetectObjectInScene(imageObjectData, imageSceneData))).ToList();

        var results = await Task.WhenAll(tasks);

        return results.ToList();
    }
    
    public async Task<IList<ObjectDetectionResult>> DetectObjectInScenesAsync(byte[] 
        objectImageData, IList<byte[]> imagesSceneData) 
    { 
        IList<ObjectDetectionResult> results = new List<ObjectDetectionResult>(); 
        results.Add(new ObjectDetectionResult 
        { ImageData = new byte[]{0},  
            Points = new List<ObjectDetectionPoint> { new() {X=1,Y=2} }} ); 
        results.Add(new ObjectDetectionResult 
        { ImageData = new byte[]{0},  
            Points = new List<ObjectDetectionPoint> { new() {X=1,Y=2} }} ); 
        return results; 
    } 
    
    imageSceneData) 
    { 
        using var imgobject = Mat.FromImageData(imageObjectData, ImreadModes.Color); 
        using var imgScene = Mat.FromImageData(imageSceneData, ImreadModes.Color); 
        using var orb = ORB.Create(10000); 
        using var descriptors1 = new Mat(); 
        using var descriptors2 = new Mat(); 
        orb.DetectAndCompute(imgobject, null, out var keyPoints1, descriptors1); 
        orb.DetectAndCompute(imgScene, null, out var keyPoints2, descriptors2); 
        using var bf = new BFMatcher(NormTypes.Hamming, crossCheck: true); 
        var matches = bf.Match(descriptors1, descriptors2); 
        var goodMatches = matches 
            .OrderBy(x => x.Distance) 
            .Take(10) 
            .ToArray();
     
} 