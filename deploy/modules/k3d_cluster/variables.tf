variable "cluster_name" {
    type        = string
    default     = "localdev"
    description = "Unique name of local cluster"  
}

variable "servers" {
    type        = number
    default     = 1
    description = "No of local API controllers instance"  
}

variable "nodes" {
    type        = number
    default     = 3
    description = "No of worker nodes"  
}

variable "switch_context" {
    description = "Should we set the .kubeconfig to this cluster"
    type        = bool
    default     = true
}