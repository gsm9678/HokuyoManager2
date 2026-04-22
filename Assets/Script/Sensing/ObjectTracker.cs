using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectTracker
{
    private const int MissingFrameLimit = 10;
    private int nextId = 1;

    public void UpdateTracks(List<DetectedCluster> detections, List<TrackedObject> tracks)
    {
        if (tracks.Count > 0 && nextId <= tracks.Max(track => track.Id))
            nextId = tracks.Max(track => track.Id) + 1;

        float epsilon = Mathf.Max(1f, GameManager.instance.data.ScaleSizeData.Epsilon);
        float matchDistance = epsilon * 2f;
        float mergeDistance = epsilon * 2.5f;
        float matchDistanceSq = matchDistance * matchDistance;
        float mergeDistanceSq = mergeDistance * mergeDistance;

        PredictTracks(tracks);

        bool[] detectionUsed = new bool[detections.Count];
        bool[] trackUsed = new bool[tracks.Count];

        MarkMergedDetections(detections, tracks, detectionUsed, trackUsed, mergeDistanceSq, epsilon);
        MatchActiveDetections(detections, tracks, detectionUsed, trackUsed, matchDistanceSq);
        AgeUnmatchedTracks(tracks, trackUsed);
        CreateNewTracks(detections, tracks, detectionUsed);
    }

    private void PredictTracks(List<TrackedObject> tracks)
    {
        for (int i = 0; i < tracks.Count; i++)
            tracks[i].PredictedPosition = tracks[i].Position + tracks[i].Velocity;
    }

    private void MarkMergedDetections(
        List<DetectedCluster> detections,
        List<TrackedObject> tracks,
        bool[] detectionUsed,
        bool[] trackUsed,
        float mergeDistanceSq,
        float epsilon)
    {
        int liveTrackCount = tracks.Count(track => track.State != TrackState.Lost);

        for (int detectionIndex = 0; detectionIndex < detections.Count; detectionIndex++)
        {
            DetectedCluster detection = detections[detectionIndex];
            List<int> nearbyTracks = new List<int>();

            for (int trackIndex = 0; trackIndex < tracks.Count; trackIndex++)
            {
                if (tracks[trackIndex].State == TrackState.Lost)
                    continue;

                float distanceSq = (tracks[trackIndex].PredictedPosition - detection.Center).sqrMagnitude;
                if (distanceSq <= mergeDistanceSq)
                    nearbyTracks.Add(trackIndex);
            }

            bool looksMerged = nearbyTracks.Count > 1 && (detections.Count < liveTrackCount || detection.Radius >= epsilon * 0.5f);
            if (!looksMerged)
                continue;

            detectionUsed[detectionIndex] = true;

            for (int i = 0; i < nearbyTracks.Count; i++)
            {
                int trackIndex = nearbyTracks[i];
                TrackedObject track = tracks[trackIndex];
                track.Position = track.PredictedPosition;
                track.State = TrackState.Merged;
                track.MissingFrames = 0;
                track.MergedFrames++;
                track.LastSeenTime = Time.time;
                trackUsed[trackIndex] = true;
            }
        }
    }

    private void MatchActiveDetections(
        List<DetectedCluster> detections,
        List<TrackedObject> tracks,
        bool[] detectionUsed,
        bool[] trackUsed,
        float matchDistanceSq)
    {
        List<MatchCandidate> candidates = new List<MatchCandidate>();

        for (int detectionIndex = 0; detectionIndex < detections.Count; detectionIndex++)
        {
            if (detectionUsed[detectionIndex])
                continue;

            for (int trackIndex = 0; trackIndex < tracks.Count; trackIndex++)
            {
                if (trackUsed[trackIndex] || tracks[trackIndex].State == TrackState.Lost)
                    continue;

                float distanceSq = (tracks[trackIndex].PredictedPosition - detections[detectionIndex].Center).sqrMagnitude;
                if (distanceSq <= matchDistanceSq)
                    candidates.Add(new MatchCandidate(trackIndex, detectionIndex, distanceSq));
            }
        }

        candidates.Sort((a, b) => a.Cost.CompareTo(b.Cost));

        for (int i = 0; i < candidates.Count; i++)
        {
            MatchCandidate candidate = candidates[i];
            if (trackUsed[candidate.TrackIndex] || detectionUsed[candidate.DetectionIndex])
                continue;

            UpdateTrack(tracks[candidate.TrackIndex], detections[candidate.DetectionIndex].Center, TrackState.Active);
            trackUsed[candidate.TrackIndex] = true;
            detectionUsed[candidate.DetectionIndex] = true;
        }
    }

    private void CreateNewTracks(List<DetectedCluster> detections, List<TrackedObject> tracks, bool[] detectionUsed)
    {
        for (int i = 0; i < detections.Count; i++)
        {
            if (detectionUsed[i])
                continue;

            tracks.Add(new TrackedObject
            {
                Id = nextId++,
                Position = detections[i].Center,
                PredictedPosition = detections[i].Center,
                Velocity = Vector2.zero,
                State = TrackState.Active,
                MissingFrames = 0,
                MergedFrames = 0,
                LastSeenTime = Time.time
            });
        }
    }

    private void AgeUnmatchedTracks(List<TrackedObject> tracks, bool[] trackUsed)
    {
        for (int i = tracks.Count - 1; i >= 0; i--)
        {
            if (i < trackUsed.Length && trackUsed[i])
                continue;

            TrackedObject track = tracks[i];
            track.MissingFrames++;

            if (track.MissingFrames >= MissingFrameLimit)
            {
                tracks.RemoveAt(i);
                continue;
            }

            track.Position = track.PredictedPosition;
            track.State = TrackState.Occluded;
        }
    }

    private void UpdateTrack(TrackedObject track, Vector2 newPosition, TrackState state)
    {
        track.Velocity = newPosition - track.Position;
        track.Position = newPosition;
        track.PredictedPosition = newPosition + track.Velocity;
        track.State = state;
        track.MissingFrames = 0;
        track.MergedFrames = state == TrackState.Merged ? track.MergedFrames + 1 : 0;
        track.LastSeenTime = Time.time;
    }

    private struct MatchCandidate
    {
        public readonly int TrackIndex;
        public readonly int DetectionIndex;
        public readonly float Cost;

        public MatchCandidate(int trackIndex, int detectionIndex, float cost)
        {
            TrackIndex = trackIndex;
            DetectionIndex = detectionIndex;
            Cost = cost;
        }
    }
}
