// Code written by Jael Ruiz
// 
// This file defines a simple configuration service that stores
// Supabase connection credentials used throughout the application.
// Other services reference this class to access the Supabase URL and API key.

namespace Dashboard.Services;

/// <summary>
/// Provides centralized access to Supabase connection settings,
/// including the base URL and the anonymous API key.
/// 
/// This class is referenced by other service classes (such as <see cref="FacultyService"/> 
/// and <see cref="EWeekGalleryService"/>) to authenticate requests to the Supabase backend.
/// </summary>
public static class SupabaseService
{
    /// <summary>
    /// The base URL of the Supabase project.
    /// Used to build REST and Storage API request endpoints.
    /// </summary>
    public static readonly string SupabaseUrl = "https://kvvoooyijzvxxnejykjv.supabase.co";

    /// <summary>
    /// The public (anonymous) API key used for unauthenticated access to Supabase.
    /// 
    /// ?? Note: This key should only be used in client-safe contexts (such as read-only public data).
    /// For secure or administrative actions, use service role keys stored in server-side configuration.
    /// </summary>
    public static readonly string SupabaseAnonKey = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6Imt2dm9vb3lpanp2eHhuZWp5a2p2Iiwicm9sZSI6ImFub24iLCJpYXQiOjE3NTg0ODgwMzgsImV4cCI6MjA3NDA2NDAzOH0.e1QW3QnzRslh3CnqfiLmFR-SCLDW6BvraJ1x11_5RqA";
}
