terraform {
    required_providers {
        k3d = {  
            source = "3rein/k3d"
            version = "0.0.4"          
        }           
    }
}

resource "k3d_cluster" "local"{
  # provider = k3dcluster
  name    = var.cluster_name
  servers = var.servers
  agents  = var.nodes

  k3d {
    disable_load_balancer     = false
    disable_image_volume      = false
    disable_host_ip_injection = false
  }

  kubeconfig {
    update_default_kubeconfig = var.switch_context
    switch_current_context    = var.switch_context
  }
}
